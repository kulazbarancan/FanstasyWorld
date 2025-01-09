using UnityEngine;

namespace DefaultNamespace
{
    [System.Serializable]
    public class Chair : MonoBehaviour
    {
        public bool isEmpty = true; // Sandalyenin boş olup olmadığını belirtir
        public Transform customerPosition; // Müşterinin oturacağı pozisyon

        /// <summary>
        /// Sandalyeyi müşteriye rezerve eder.
        /// </summary>
        public void ReserveChair()
        {
            isEmpty = false;
        }

        /// <summary>
        /// Sandalyeyi boşaltır.
        /// </summary>
        public void FreeChair()
        {
            isEmpty = true;
        }

        /// <summary>
        /// Müşteriyi sandalyeye oturtur.
        /// </summary>
        public Vector3 GetChairPosition()
        {
            return customerPosition != null ? customerPosition.position : transform.position;
        }
    }
}