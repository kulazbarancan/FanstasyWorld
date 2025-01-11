using System;
using System.Collections.Generic;
using DefaultNamespace.Kitchen;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class RecipeManager : ScopedSingletonMonoBehaviour<RecipeManager>
{
    // Mevcut stok
    public List<Ingredient> ingredientList;
    public Dictionary<Ingredient, int> ingredientStock = new Dictionary<Ingredient, int>();
    public List<Recipe> allRecipes = new List<Recipe>();

    private void Start()
    {
        for (int i = 0; i < ingredientList.Count; i++)
        {
            AddToStock(ingredientList[i],ingredientList[i].amount);
        }
    }

    [Button]
    public void CookRandom()
    {
        CookRandomRecipe();
    }
    
    /// Add stock.
    public void AddToStock(Ingredient ingredient, int amount)
    {
        if (ingredientStock.ContainsKey(ingredient))
        {
            ingredientStock[ingredient] += amount;
        }
        else
        {
            ingredientStock[ingredient] = amount;
        }
    }
    public bool OwnsRecipe(Recipe recipe)
    {
        return recipe.hasRecipe;
    }

    /// check recipe can cook
    public bool CanCook(Recipe recipe)
    {
        if (!OwnsRecipe(recipe)) return false; //is ingredient is missing u cannot cook

        foreach (var recipeIngredient in recipe.ingredients)
        {
            if (!ingredientStock.ContainsKey(recipeIngredient.ingredient) || ingredientStock[recipeIngredient.ingredient] < recipeIngredient.amount)
            {
                return false; // missing ingredient
            }
        }
        
        return true; // have all ingredient
    }
    
    public bool Cook(Recipe recipe)
    {
        if (CanCook(recipe))
        {
            foreach (var recipeIngredient in recipe.ingredients)
            {
                ingredientStock[recipeIngredient.ingredient] -= recipeIngredient.amount;
            }
            Debug.Log($"{recipe.recipeName} cooked!");
            return true;
        }
        else
        {
            Debug.Log($"Cook cannot: {recipe.recipeName}");
            return false;
        }
    }

    public void CookRandomRecipe()
    {
        // Yemek yapılabilecek tarifleri filtrele
       

        foreach (var recipe in allRecipes)
        {
            if (CanCook(recipe))
            {
                //cookableRecipes.Add(recipe);
                Debug.Log("Yemek yapilabilir");
            }
        }

        // Eğer yemek yapılabilecek tarif yoksa mesaj ver
        if (allRecipes.Count == 0)
        {
            Debug.Log("Yemek yapılabilecek tarif bulunamadı.");
            return;
        }

        // Rastgele bir tarif seç ve yap
        int randomIndex = Random.Range(0, allRecipes.Count);
        Cook(allRecipes[randomIndex]);
    }
}