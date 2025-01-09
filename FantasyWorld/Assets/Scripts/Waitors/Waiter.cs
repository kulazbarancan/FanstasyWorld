using System.Collections;
using Customers;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.Waitors
{
    public class Waiter : MonoBehaviour
    {
        public float speed = .1f; // Garson hareket hızı
        public bool isBusy = false; // Garsonun meşgul olup olmadığını kontrol eder
        public Transform target; // Garsonun gitmek istediği hedef (müşteri)

        private void OnEnable()
        {
            CustomerController.OnCustomerReadyToGo += AssignCustomerToWaiter; // Müşteri event'ine abone ol
        }

        private void OnDisable()
        {
            CustomerController.OnCustomerReadyToGo -= AssignCustomerToWaiter; // Event'ten çık
        }

        private void AssignCustomerToWaiter(Customer customer)
        {
            if (isBusy || customer == null || customer.waiter != null)
                return; // Garson meşgulse veya müşteri zaten işleniyorsa işlem yapma

            // Garsonu müşteriye ata
            customer.waiter = this; // Bu garson müşteriye atanır
            StartCoroutine(HandleCustomerOrder(customer)); // İşlemi başlat
        }

        private IEnumerator HandleCustomerOrder(Customer customer)
        {
            isBusy = true;

            Debug.Log("Normal");
            // Müşteriye git
            target = customer.transform;
            yield return new WaitUntil(() => customer.customerOnTable);
            yield return MoveToTarget(target.position);

            // Müşteriyle ilgilen
            customer.waiterOnTable = true;
            if (customer.angryCustomer)
            {
                Debug.Log("ANGRY CUSTOMER FOUND");
                MoveTarget(WaiterController.Instance.waiterExitPos.position);
                yield break;
            }
            yield return new WaitForSeconds(5); // Müşteriye hizmet et

            // Mutfağa git
            yield return MoveToTarget(WaiterController.Instance.kitchenPos.position);

            yield return new WaitForSeconds(5); // Yemek hazırlama süresi

            // Müşteriye geri dön
            if (target != null)
            {
                yield return MoveToTarget(target.position);
            }

            yield return new WaitForSeconds(2); // Müşteriye yemek teslim süresi

            // Çıkış pozisyonuna git ve meşgul durumunu sıfırla
            yield return MoveToTarget(WaiterController.Instance.waiterExitPos.position);

            isBusy = false; // Garson tekrar boşta
        }

        private void MoveTarget(Vector3 targetPosition)
        {
            transform.DOMove(targetPosition, 1f); // Hedefe hareket et
        }

        private IEnumerator MoveToTarget(Vector3 targetPosition)
        {
            transform.DOMove(targetPosition, 1f); // Hedefe hareket et
            yield return new WaitForSeconds(1f); // Hareket tamamlanana kadar bekle
        }
    }
}