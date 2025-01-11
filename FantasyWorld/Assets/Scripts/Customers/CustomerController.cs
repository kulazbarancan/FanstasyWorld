using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Customers
{
    public class CustomerController : MonoBehaviour
    {
        public static CustomerController Instance;
        public static event Action<Customer> OnCustomerReadyToGo;
        public List<CustomerSO> customerSo;
        public List<Customer> activeCustomerList = new List<Customer>();
        public Transform spawnPoint; // new Customer spawn point
        public Transform customerExitPos;
        public List<Chair> allChairs;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void AddNewCustomer()
        {
            // Create new customer
            Chair availableChair = FindAvailableChair();
            if (availableChair == null)
            {
                return;
            }
            var random = Random.Range(0, customerSo.Count);
            GameObject newCustomerObj = Instantiate(customerSo[random].customerPrefab.gameObject, spawnPoint.position,
                Quaternion.identity);
            Customer newCustomer = newCustomerObj.GetComponent<Customer>();
            newCustomer.customerSO = customerSo[random];

            if (newCustomer != null)
            {
                // add Customer to active list
                activeCustomerList.Add(newCustomer);

                // start newCustomer
                newCustomer.targetChair = GetAvailableChair();
                newCustomer.customerChair = newCustomer.targetChair.GetComponent<Chair>();
                newCustomer.customerExitPos = customerExitPos;

                // Send event to waiter for new Customer
                OnCustomerReadyToGo?.Invoke(newCustomer);
            }
            else
            {
                Debug.LogError("Missing script in Customer prefab.!");
            }
        }

        private Chair FindAvailableChair()
        {
            return allChairs.Where(chair => chair.isEmpty).FirstOrDefault();
        }
        private Transform GetAvailableChair()
        {
            // Look all chars and find empty one
            foreach (Chair chair in allChairs)
            {
                if (chair.isEmpty)
                {
                    chair.ReserveChair(); // Fill the chair
                    return chair.customerPosition != null ? chair.customerPosition : chair.transform;
                }
            }

            Debug.LogWarning("There is no empty chair!");
            return null;
        }

        public void RemoveCustomer(Customer customer)
        {
            if (activeCustomerList.Contains(customer))
            {
                activeCustomerList.Remove(customer);
            }
        }

        [Button]
        public void CallCustomer()
        {
            AddNewCustomer();
        }
    }
}