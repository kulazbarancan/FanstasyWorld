using UnityEngine;

namespace DefaultNamespace
{
    [System.Serializable]
    public class Chair
    {
        public bool isEmpty;
        public GameObject chair;

        public bool CheckChairStatus()
        {
            return isEmpty;
        }
    }
}