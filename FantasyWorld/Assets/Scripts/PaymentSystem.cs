using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Customers
{
    public class PaymentSystem : ScopedSingletonMonoBehaviour<PaymentSystem>
    {
        public Transform paymentPoint; // PaymentPos
        public Transform paymentQueueStartPoint; // Payment start pos
        public float queueSpacing = 1.5f; // gaps between customers which one on the line

        private Queue<Customer> paymentQueue = new Queue<Customer>(); // customers on the payment que
        private bool isProcessingPayment = false; // check payment pros

 
        public void AddToPaymentQueue(Customer customer)
        {
            //add customer to payment que
            if (!paymentQueue.Contains(customer))
            {
                paymentQueue.Enqueue(customer);
                Debug.Log($"{customer.name} ödeme kuyruğuna eklendi.");
                ArrangeQueuePositions(); // arrange que pos
                if (!isProcessingPayment) StartCoroutine(ProcessPaymentQueue());
            }
        }

      
        private IEnumerator ProcessPaymentQueue()
        {
            isProcessingPayment = true;

            while (paymentQueue.Count > 0)
            {
                // take next customer
                Customer currentCustomer = paymentQueue.Dequeue();

                // customer going to payment pos
                yield return currentCustomer.transform.DOMove(paymentPoint.position,1f);

                // payment time simulation for example 2 sec
                Debug.Log($"{currentCustomer.name} payment time...");
                yield return new WaitUntil(() => currentCustomer.hasPaid);
                yield return new WaitForSeconds(2f);

                // after hasPaid true customer leave restaurant
                currentCustomer.LeaveRestaurant();

                // arrange que again
                ArrangeQueuePositions();
            }

            isProcessingPayment = false;
        }

        private void ArrangeQueuePositions()
        {
            int index = 0;
            foreach (var customer in paymentQueue)
            {
                Vector3 queuePosition = paymentQueueStartPoint.position + Vector3.back * queueSpacing * index;
                customer.transform.DOMove(queuePosition,1f);
                index++;
            }
        }
    }
}
