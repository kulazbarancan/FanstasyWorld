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
        
        public Chair customerChair;
        public Transform targetChair;
        public Transform customerExitPos;

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
              StartCoroutine(nameof(StartCustomerLifeTime));
            });
        }

        
        public IEnumerator StartCustomerLifeTime()
        {
            var randomTime = Random.Range(0, 50);
            yield return new WaitForSeconds(1);
            DecideWhatYouWantToDo();
        }

        private void DecideWhatYouWantToDo()
        {
            transform.DOMove(customerExitPos.position, 1f).OnComplete(() =>
            {
                customerChair.isEmpty = true;
            });
        }
    }
}