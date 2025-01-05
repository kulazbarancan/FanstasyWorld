using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Kitchen
{
    [CreateAssetMenu(fileName = "NewRecipe", menuName = "ScriptableObjects/Recipe", order = 2)]
    public class Recipe : ScriptableObject
    {
        public string recipeName; // Yemek adı
        public Sprite recipeImage; // Yemek görseli (UI için)
        public float preparationTime; // Hazırlık süresi
        public List<RecipeIngredient> ingredients; // Gerekli malzemeler listesi
    }

    [System.Serializable]
    public class RecipeIngredient
    {
        public Ingredient ingredient; // Gerekli malzeme
        public int amount; // Malzeme miktarı
    }
}