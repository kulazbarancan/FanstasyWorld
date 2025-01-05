using System;
using System.Collections;
using System.Collections.Generic;
using Customers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.Waitors
{
    public class WaiterController : MonoBehaviour
    {
        public GameObject waiterPrefab;
        public Transform waiterExitPos;
        public Transform waiterStartPos;
        public int totalWaiterCount;
        public List<Waiter> totalWaiters;

        private void Awake()
        {
            for (var i = 0; i < totalWaiterCount; i++)
            {
                var newWaiter = Instantiate(waiterPrefab, waiterStartPos.position, Quaternion.identity);
                totalWaiters.Add(newWaiter.GetComponent<Waiter>());
            }
        }

        private void OnEnable()
        {
            Customer.OnWaiterReadyToGo += CheckWaiters;
        }

        private void OnDisable()
        {
            Customer.OnWaiterReadyToGo -= CheckWaiters;
        }

        public void CheckWaiters()
        {
            StartCoroutine(CheckAvailableWaiter());
        }

        public IEnumerator CheckAvailableWaiter()
        {
            yield return new WaitForSeconds(1f);
            foreach (var waiter in totalWaiters)
            {
                waiter.CheckStatus();
                if (!waiter.isBusy)
                {
                    foreach (var customer in CustomerController.Instance.activeCustomerList)
                    {
                        if (!customer.alreadyOrdered)
                        {
                            waiter.target = customer.transform;
                            waiter.transform.DOMove(waiter.target.position, 1f);
                            waiter.isBusy = true;
                            customer.alreadyOrdered = true;
                            yield return new WaitForSeconds(5);
                            waiter.transform.DOMove(waiterExitPos.position, 1f);
                        }
                    }
                }
            }
        }

        public void StartOrderWaiter()
        {
            
        }

    }
}