using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Kitchen
{
    [CreateAssetMenu(fileName = "NewRecipe", menuName = "ScriptableObjects/Recipe", order = 2)]
    public class Recipe : ScriptableObject
    {
        public float price;
        public bool hasRecipe;
        public string recipeName; // FoodName
        public Sprite recipeImage; // Food ui image
        public float preparationTime; // PreparationTime
        public List<RecipeIngredient> ingredients; // ingredient list
    }

    [System.Serializable]
    public class RecipeIngredient
    {
        public Ingredient ingredient; // neccessary ingredients
        public int amount; // ingredient amount
    }
}