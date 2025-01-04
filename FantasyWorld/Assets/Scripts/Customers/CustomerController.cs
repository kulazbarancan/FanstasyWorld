using System;
using System.Collections;
using System.Collections.Generic;
using Customers;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerController : MonoBehaviour
{
    public static event Action<Customer> OnCustomerReadyToGo;
    
    public Transform customerExitPos;
    public Transform customerStartPos;
    public List<CustomerSO> customerList;

    private void OnEnable()
    {
        ChairController.OnCustomerRequested += HandleCustomerRequest;
    }

    private void OnDisable()
    {
        ChairController.OnCustomerRequested -= HandleCustomerRequest;
    }

    public void HandleCustomerRequest(Chair chair)
    {
        var randomCustomer = Random.Range(0, customerList.Count);
        var newCustomer = Instantiate(customerList[randomCustomer].customerPrefab, customerStartPos.position, Quaternion.identity);
        newCustomer.targetChair = chair.chair.transform;
        newCustomer.customerExitPos = customerExitPos;
        newCustomer.customerChair = chair;
        newCustomer.Initialize(customerList[randomCustomer]);
        OnCustomerReadyToGo.Invoke(newCustomer);
        chair.isEmpty = true;
        Debug.Log("CAGIRILDI");
    }
}
