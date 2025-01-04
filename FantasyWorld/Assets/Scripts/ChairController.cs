using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace DefaultNamespace
{
    public class ChairController : MonoBehaviour
    {
        public static event Action<Chair> OnCustomerRequested;
        public List<Chair> Chairs;
        
        [Button]
        public void CallCustomer()
        {
            foreach (Chair chair in Chairs)
            {
                chair.CheckChairStatus();
                if (!chair.isEmpty)
                {
                    Debug.Log("Request Sended");
                    OnCustomerRequested.Invoke(chair);
                    //There is a empty chair in shop
                }
            }
        }
    }
}