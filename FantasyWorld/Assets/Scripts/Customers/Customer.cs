using System;
using System.Collections;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Customers
{
    public class Customer : MonoBehaviour
    {
        public static event Action OnWaiterReadyToGo;
        
        public GameObject customerAttentionObject;
        public CustomerSO customerSO;
        public Chair customerChair;
        public Transform targetChair;
        public Transform customerExitPos;
        public bool alreadyOrdered;

        public float speed;
        public float money;
        public int alcoholLimit;

        public void Initialize(CustomerSO So)
        {
            customerSO = So;
            alcoholLimit = So.alcoholLimit;
            speed = So.customerSpeed;
            money = So.customerPaymentBehiaviorState switch
            {
                CustomerMoneyTypeEnum.Normal => Random.Range(1, 100),
                CustomerMoneyTypeEnum.Rich => Random.Range(100, 300),
                CustomerMoneyTypeEnum.HasMoney => Random.Range(50, 150),
                CustomerMoneyTypeEnum.UltraRich => Random.Range(300, 1000),
                _ => money
            };
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
            transform.DOMove(targetChair.position, 1f).OnComplete(() =>
            {
                StartCoroutine(nameof(StartOrder));
            });
        }

        private IEnumerator StartOrder()
        {
            var randomTime = Random.Range(15, 30);
            customerAttentionObject.SetActive(true);
            OnWaiterReadyToGo.Invoke();
            switch (customerSO.customerPaymentBehiaviorState)
            {
                case CustomerMoneyTypeEnum.Normal:
                    break;
                case CustomerMoneyTypeEnum.HasMoney:
                    break;
                case CustomerMoneyTypeEnum.Rich:
                    break;
                case CustomerMoneyTypeEnum.UltraRich:
                    break;
            }
            yield return new WaitForSeconds(randomTime);
            DecideWhatYouWantToDo();
        }
        private void DecideWhatYouWantToDo()
        {
            switch (customerSO.customerBehiaviorState)
            {
                case CustomerBehaivorEnum.Fight:
                    Debug.Log("Fight");
                    break;
                case CustomerBehaivorEnum.Pay:
                    Debug.Log("Pay");
                    break;
                case CustomerBehaivorEnum.RunNotPay:
                    Debug.Log("RunNotPay");
                    break;
            }
            transform.DOMove(customerExitPos.position, 1f).OnComplete(() =>
            {
                customerChair.isEmpty = false;
                CustomerController.Instance.activeCustomerList.Remove(this);
                Destroy(gameObject);
            });
        }
    }
}