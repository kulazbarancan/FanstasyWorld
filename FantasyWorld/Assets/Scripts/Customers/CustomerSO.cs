using UnityEngine;

namespace Customers
{
    [CreateAssetMenu(fileName = "Customer", menuName = "ScriptableObjects/Customer")]
    public class CustomerSO : ScriptableObject
    {
        public enum CustomerPaymentBehiaviorState
        {
            Pay,
            RunNotPay,
            Fight,
        }
        public enum CustomerFinancialState
        {
            Normal,
            HasMove,
            Rich,
            UltraRich
        }
        public Customer customerPrefab;
        public float customerSpeed;
    }
}