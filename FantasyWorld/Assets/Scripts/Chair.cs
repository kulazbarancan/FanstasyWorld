using System;
using Customers;
using UnityEngine;

namespace DefaultNamespace
{
    [System.Serializable]
    public class Chair : MonoBehaviour
    {
        public bool isEmpty = true; // look chair is empty or not
        public Transform customerPosition; // Customer sit pos

        public void ReserveChair()
        {
            isEmpty = false;
        }

        private void Awake()
        {
            CustomerController.Instance.allChairs.Add(this);
        }

     
        public void FreeChair()
        {
            isEmpty = true;
        }

      
        public Vector3 GetChairPosition()
        {
            return customerPosition != null ? customerPosition.position : transform.position;
        }
    }
}