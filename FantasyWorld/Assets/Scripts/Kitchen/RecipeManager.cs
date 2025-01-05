using System.Collections.Generic;
using DefaultNamespace.Kitchen;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    // Mevcut stok
    public Dictionary<Ingredient, int> ingredientStock = new Dictionary<Ingredient, int>();
    public List<Recipe> ownedRecipes = new List<Recipe>();

    
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
        return ownedRecipes.Contains(recipe);
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
        List<Recipe> cookableRecipes = new List<Recipe>();

        foreach (var recipe in ownedRecipes)
        {
            if (CanCook(recipe))
            {
                cookableRecipes.Add(recipe);
            }
        }

        // Eğer yemek yapılabilecek tarif yoksa mesaj ver
        if (cookableRecipes.Count == 0)
        {
            Debug.Log("Yemek yapılabilecek tarif bulunamadı.");
            return;
        }

        // Rastgele bir tarif seç ve yap
        int randomIndex = Random.Range(0, cookableRecipes.Count);
        Cook(cookableRecipes[randomIndex]);
    }
}