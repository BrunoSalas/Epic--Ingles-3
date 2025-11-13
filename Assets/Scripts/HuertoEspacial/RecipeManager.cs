using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public Dictionary<Products, List<string>> plantRecipes = new Dictionary<Products, List<string>>();

    void Start()
    {
        InitializeRecipes();
    }

    void InitializeRecipes()
    {
        plantRecipes.Add(Products.colMorada, new List<string> { "algo con colMorada" });
        plantRecipes.Add(Products.colBlanca, new List<string> { "algo con colBlanca" });
        plantRecipes.Add(Products.lechugaRepollo, new List<string> { "tacos" });
        plantRecipes.Add(Products.lechugaCrespa, new List<string> { "Bocaditos  para 10 personas.." });
        plantRecipes.Add(Products.remolacha, new List<string> { "Salsa de remolacha" });
        plantRecipes.Add(Products.culantro, new List<string> { "algo con culantro" });
        plantRecipes.Add(Products.perejil, new List<string> { "algo con perejil" });
        plantRecipes.Add(Products.apio, new List<string> { "Crema de apio con queso mozzarella, Palitos de apio" });
        plantRecipes.Add(Products.calabaza, new List<string> { "algo con calabaz" });
        plantRecipes.Add(Products.pimiento, new List<string> { "algo con pimiento" });
        plantRecipes.Add(Products.zanahoria, new List<string> { "algo con zanahoria" });
        plantRecipes.Add(Products.acelga, new List<string> { "Ensalada de acelga, Tortilla de acelgas con huevos" });


    }

    public List<string> GetRecipes(Products plant)
    {
        if (plantRecipes.ContainsKey(plant))
        {
            return plantRecipes[plant];
        }
        return new List<string>();
    }
}
