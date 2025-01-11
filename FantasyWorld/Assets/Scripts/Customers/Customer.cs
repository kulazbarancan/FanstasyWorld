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
      
        public bool waiterOnTable;
        public bool customerOnTable;
        public bool angryCustomer;
        public bool hasPaid;

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
            if (customer != this) return; // just target himself
            transform.DOMove(targetChair.position, 1f).OnComplete(() =>
            {
                StartCoroutine(nameof(StartOrder));
            });
        }

        private IEnumerator StartOrder()
        {
            // Start customer call waiter
            customerAttentionObject.SetActive(true);
            customerOnTable = true;
            var selectedRecipe = GetRandomRecipe();
            orderedRecipes.Add(selectedRecipe);

            if (RecipeManager.Instance.CanCook(selectedRecipe))
            {
                yield return new WaitUntil(() => waiterOnTable); // wait until waiter on table

                Debug.Log($"Cooking {selectedRecipe.recipeName}");
                RecipeManager.Instance.Cook(selectedRecipe); // Cook the orders

                yield return new WaitForSeconds(Random.Range(15, 30)); // waiting time
                DecideWhatYouWantToDo(); // Customer decide
            }
            else
            {
                // if the is no ingredient for orders. Waiter will be going back start pos 
                angryCustomer = true;
                yield return new WaitUntil(() => waiterOnTable); // wait until waiter on table
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
                    PaymentSystem.Instance.AddToPaymentQueue(this);
                    break;
                case CustomerBehaivorEnum.RunNotPay:
                    Debug.Log("Customer ran away without paying!");
                    break;
            }

           // LeaveRestaurant();
        }

        public void LeaveRestaurant()
        {
            transform.DOMove(customerExitPos.position, 1f).OnComplete(() =>
            {
                // make chair empty
                if (customerChair != null)
                {
                    customerChair.FreeChair();
                }

                CustomerController.Instance.RemoveCustomer(this); // Remowe customer from list
                Destroy(gameObject); // Destroy customer
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
    }
}
