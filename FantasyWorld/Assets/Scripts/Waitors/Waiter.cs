using UnityEngine;

namespace DefaultNamespace.Waitors
{
    public class Waiter : MonoBehaviour
    {
        public float speed;
        public bool isBusy;
        public Transform target;

        public bool CheckStatus()
        {
            return isBusy;
        }
    }
}