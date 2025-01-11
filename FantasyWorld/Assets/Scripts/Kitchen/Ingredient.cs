using UnityEngine;

namespace DefaultNamespace.Kitchen
{
    [CreateAssetMenu(fileName = "NewIngredient", menuName = "ScriptableObjects/Ingredient", order = 1)]
    public class Ingredient : ScriptableObject
    {
        public int amount;
        public string ingredientName;   // ingredientName
        public Sprite ingredientIcon;   // ingredient image
        public float price;             // ingredient price
    }
}