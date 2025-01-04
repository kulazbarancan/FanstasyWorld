using UnityEngine;

namespace DefaultNamespace
{
    public class ShopController : SingletonMonoBehaviour<ShopController>
    {
        public int shopCapacity;
        public bool isShopOpen;
    }
}