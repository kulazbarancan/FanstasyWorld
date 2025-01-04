using UnityEngine;

namespace Customers
{
    [CreateAssetMenu(fileName = "Customer", menuName = "ScriptableObjects/Customer")]
    public class CustomerSO : ScriptableObject
    {
        public CustomerBehaivorEnum customerBehiaviorState;
        public CustomerMoneyTypeEnum customerPaymentBehiaviorState;
        public Customer customerPrefab;
        public float customerSpeed;
        public int alcoholLimit;
    }
}