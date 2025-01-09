using System.Collections.Generic;
using Customers;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace DefaultNamespace
{
    public class PaymentSystem : ScopedSingletonMonoBehaviour<PaymentSystem>
    {
        public Transform denemePos;
        public List<Customer> paymentCustomerQueue;
        public Customer currentCustomer;
        public Transform paymentContainer;

        public void MakePayment(Customer customer)
        {
            currentCustomer = customer;
            if (currentCustomer != null && !customer.hasPaid)
            {
                paymentCustomerQueue.Add(customer);
                customer.transform.DOMove(paymentContainer.position, 1f);
                customer.MakePayment();
            }
        }
        public float distanceBetweenNPCs = 2.0f;  // NPC'ler arasındaki mesafe
[Button]
        public void Deneme()
        {
            for (int i = 0; i < 10; i++)
            {
                var go = Instantiate(CustomerController.Instance.customerSo[i].customerPrefab,denemePos.position,Quaternion.identity);
                paymentCustomerQueue.Add(go);
            }

            float startXPosition = 0f;  // Başlangıç X pozisyonu
            for (int i = 0; i < paymentCustomerQueue.Count; i++)
            {
                paymentCustomerQueue[i].transform.position = new Vector3(startXPosition, paymentCustomerQueue[i].transform.position.y, paymentCustomerQueue[i].transform.position.z);
                startXPosition += distanceBetweenNPCs;  // Sonraki NPC'nin X pozisyonu
            }
        }
    }
}