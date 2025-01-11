using System.Collections;
using Customers;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.Waitors
{
    public class Waiter : MonoBehaviour
    {
        public float speed = .1f; // waiter speed
        public bool isBusy = false; // waiter busy or not
        public Transform target; // waiters target (customer)

        private void OnEnable()
        {
            CustomerController.OnCustomerReadyToGo += AssignCustomerToWaiter; 
        }

        private void OnDisable()
        {
            CustomerController.OnCustomerReadyToGo -= AssignCustomerToWaiter;
        }

        private void AssignCustomerToWaiter(Customer customer)
        {
            if (isBusy || customer == null || customer.waiter != null)
                return; // is waiter is busy return

            // assign waiter
            customer.waiter = this;
            StartCoroutine(HandleCustomerOrder(customer)); // start process
        }

        private IEnumerator HandleCustomerOrder(Customer customer)
        {
            isBusy = true;

            Debug.Log("Normal");
            // go to customer
            target = customer.transform;
            yield return new WaitUntil(() => customer.customerOnTable);
            yield return MoveToTarget(target.position);

            // look customer what he want
            customer.waiterOnTable = true;
            if (customer.angryCustomer)
            {
                Debug.Log("ANGRY CUSTOMER FOUND");
                MoveTarget(WaiterController.Instance.waiterExitPos.position);
                yield break;
            }
            yield return new WaitForSeconds(5); // serve customer

            // go to kitchen
            yield return MoveToTarget(WaiterController.Instance.kitchenPos.position);

            yield return new WaitForSeconds(5); // wait food until prepare

            // get back to customer
            if (target != null)
            {
                yield return MoveToTarget(target.position);
            }

            yield return new WaitForSeconds(2); // wait 2 sec after back customer

            // back to start pos
            yield return MoveToTarget(WaiterController.Instance.waiterExitPos.position);

            isBusy = false; // busy false
        }

        private void MoveTarget(Vector3 targetPosition)
        {
            transform.DOMove(targetPosition, 1f);
        }

        private IEnumerator MoveToTarget(Vector3 targetPosition)
        {
            transform.DOMove(targetPosition, 1f); 
            yield return new WaitForSeconds(1f); 
        }
    }
}