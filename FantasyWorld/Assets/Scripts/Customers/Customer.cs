using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Kitchen;
using DefaultNamespace.Waitors;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Customers
{
    public class Customer : MonoBehaviour
    {
        public List<Recipe> orderedRecipes = new List<Recipe>();
        public GameObject customerAttentionObject;
        public CustomerSO customerSO;
        public Chair customerChair;
        public Transform targetChair;
        public Transform customerExitPos;
        public Waiter waiter;
        public float bill;
        public float speed;
        public float money;
        public int alcoholLimit;
        public bool alreadyOrdered = false;
        public bool waiterOnTable = false;
        public bool customerOnTable = false;
        public bool angryCustomer = false;
        public bool hasPaid;

        public void Initialize(CustomerSO so)
        {
            customerSO = so;
            alcoholLimit = so.alcoholLimit;
            speed = so.customerSpeed;
            money = CalculateMoneyBasedOnType(so.customerPaymentBehiaviorState);
        }

        private void OnEnable()
        {
            CustomerController.OnCustomerReadyToGo += MoveToChair;
        }

        private void OnDisable()
        {
            CustomerController.OnCustomerReadyToGo -= MoveToChair;
        }

        public void MoveToChair(Customer customer)
        {
            if (customer != this) return; // Sadece kendisi hedef olmalı
            transform.DOMove(targetChair.position, 1f).OnComplete(() =>
            {
                StartCoroutine(nameof(StartOrder));
            });
        }

        private IEnumerator StartOrder()
        {
            // Müşterinin garson çağırmasını başlat
            customerAttentionObject.SetActive(true);
            customerOnTable = true;
            var selectedRecipe = GetRandomRecipe();
            orderedRecipes.Add(selectedRecipe);

            if (RecipeManager.Instance.CanCook(selectedRecipe))
            {
                yield return new WaitUntil(() => waiterOnTable); // Garson masaya gelene kadar bekle

                Debug.Log($"Cooking {selectedRecipe.recipeName}");
                RecipeManager.Instance.Cook(selectedRecipe); // Yemeği pişir
                bill += selectedRecipe.price;

                yield return new WaitForSeconds(Random.Range(15, 30)); // Bekleme süresi
                DecideWhatYouWantToDo(); // Müşteri kararını verir
            }
            else
            {
                // Eğer tarif pişirilemiyorsa, garson geri gönderilir
                angryCustomer = true;
                yield return new WaitUntil(() => waiterOnTable); // Garson masaya gelene kadar bekle
                LeaveRestaurant();
            }
        }

        private void DecideWhatYouWantToDo()
        {
            switch (customerSO.customerBehiaviorState)
            {
                case CustomerBehaivorEnum.Fight:
                    Debug.Log("Customer started a fight!");
                    break;
                case CustomerBehaivorEnum.Pay:
                    Debug.Log("Customer paid the bill.");
                    break;
                case CustomerBehaivorEnum.RunNotPay:
                    Debug.Log("Customer ran away without paying!");
                    break;
            }

            LeaveRestaurant();
        }

        private void LeaveRestaurant()
        {
            transform.DOMove(customerExitPos.position, 1f).OnComplete(() =>
            {
                // Sandalyeyi boşalt
                if (customerChair != null)
                {
                    customerChair.FreeChair();
                }

                CustomerController.Instance.RemoveCustomer(this); // Müşteriyi listeden çıkar
                Destroy(gameObject); // Müşteriyi yok et
            });
        }

        private float CalculateMoneyBasedOnType(CustomerMoneyTypeEnum type)
        {
            return type switch
            {
                CustomerMoneyTypeEnum.Normal => Random.Range(1, 100),
                CustomerMoneyTypeEnum.Rich => Random.Range(100, 300),
                CustomerMoneyTypeEnum.HasMoney => Random.Range(50, 150),
                CustomerMoneyTypeEnum.UltraRich => Random.Range(300, 1000),
                _ => 0
            };
        }

        private Recipe GetRandomRecipe()
        {
            var recipes = RecipeManager.Instance.allRecipes;
            return recipes[Random.Range(0, recipes.Count)];
        }

        public void MakePayment()
        {
            hasPaid = true;
            Debug.Log("ODEDIM");
        }
    }
}
