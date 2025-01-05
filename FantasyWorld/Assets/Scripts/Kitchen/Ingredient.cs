using UnityEngine;

namespace DefaultNamespace.Kitchen
{
    [CreateAssetMenu(fileName = "NewIngredient", menuName = "ScriptableObjects/Ingredient", order = 1)]
    public class Ingredient : ScriptableObject
    {
        public int amount;
        public string ingredientName;   // Malzeme adı
        public Sprite ingredientIcon;   // Malzeme görseli (UI için)
        public float price;             // Malzeme fiyatı (opsiyonel)
    }
}