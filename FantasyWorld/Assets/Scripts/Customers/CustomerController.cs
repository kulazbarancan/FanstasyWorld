using System;
using System.Collections.Generic;
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
        public Transform spawnPoint; // Yeni müşteri spawn noktası
        public GameObject customerPrefab; // Yeni müşteriler için prefab
        public Transform customerExitPos;
        public Transform paymentPos;// Müşterilerin çıkış noktası

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void AddNewCustomer()
        {
            // Yeni müşteriyi oluştur
            var random = Random.Range(0, customerSo.Count);
            GameObject newCustomerObj = Instantiate(customerSo[random].customerPrefab.gameObject, spawnPoint.position, Quaternion.identity);
            Customer newCustomer = newCustomerObj.GetComponent<Customer>();
            newCustomer.customerSO = customerSo[random];

            if (newCustomer != null)
            {
                // Yeni müşteriyi aktif listeye ekle
                activeCustomerList.Add(newCustomer);

                // Yeni müşteriyi başlat
                newCustomer.targetChair = GetAvailableChair();
                newCustomer.customerChair = newCustomer.targetChair.GetComponent<Chair>();
                newCustomer.customerExitPos = customerExitPos;

                // Tüm garsonlara müşteri geldiğini bildir
                OnCustomerReadyToGo?.Invoke(newCustomer);
            }
            else
            {
                Debug.LogError("Yeni müşteri prefab'ında Customer scripti eksik!");
            }
        }

        private Transform GetAvailableChair()
        {
            // Tüm sandalyeleri kontrol ederek boş bir sandalye bul
            foreach (Chair chair in FindObjectsOfType<Chair>())
            {
                if (chair.isEmpty)
                {
                    chair.ReserveChair(); // Sandalyeyi doldur
                    return chair.customerPosition != null ? chair.customerPosition : chair.transform;
                }
            }

            Debug.LogWarning("Boş sandalye bulunamadı!");
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
