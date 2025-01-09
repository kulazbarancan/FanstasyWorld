using System;
using System.Collections;
using System.Collections.Generic;
using Customers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.Waitors
{
    public class WaiterController : SingletonMonoBehaviour<WaiterController>
    {
        public Transform kitchenPos;
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
        
    }
}