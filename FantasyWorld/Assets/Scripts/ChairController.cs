using System;
using System.Collections;
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
        public void CallRequestCustomer()
        {
            StartCoroutine(CallCustomer());
        }


        public IEnumerator CallCustomer()
        {
            while (true)
            {
                yield return new WaitForSeconds(5f);
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
}