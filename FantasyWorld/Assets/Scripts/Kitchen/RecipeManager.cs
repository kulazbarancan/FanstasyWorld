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
    
    /// Stoğa malzeme ekler.
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

    /// Yemek yapabilecek malzeme var mı kontrol eder.
    public bool CanCook(Recipe recipe)
    {
        if (!OwnsRecipe(recipe)) return false; // Eğer tarife sahip değilsek yemek yapamayız

        foreach (var recipeIngredient in recipe.ingredients)
        {
            if (!ingredientStock.ContainsKey(recipeIngredient.ingredient) || ingredientStock[recipeIngredient.ingredient] < recipeIngredient.amount)
            {
                return false; // Malzeme eksik
            }
        }
        
        return true; // Tüm malzemeler mevcut
    }

    /// Yemek yapar ve stoktan gerekli malzemeleri düşer.
    public bool Cook(Recipe recipe)
    {
        if (CanCook(recipe))
        {
            foreach (var recipeIngredient in recipe.ingredients)
            {
                ingredientStock[recipeIngredient.ingredient] -= recipeIngredient.amount;
            }
            Debug.Log($"{recipe.recipeName} yapıldı!");
            return true;
        }
        else
        {
            Debug.Log($"Yemek yapılamadı: {recipe.recipeName}");
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