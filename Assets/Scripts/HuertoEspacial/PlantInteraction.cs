using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlantInteraction : MonoBehaviour
{
    public static PlantInteraction instance;
    public Image panel;
    public TMP_Text recipeDisplay;
    public Animator anim;
    public RecipeManager recipeManager;
    public float fadeDuration = 1.0f;

    private void Start()
    {
        instance = this;
    }
    public void DisplayPlantRecipesUI(Products plant)
    {
        List<string> recipes = recipeManager.GetRecipes(plant);
        recipeDisplay.text = "Platos con " + plant.ToString() + ":\n";
        foreach (string recipe in recipes)
        {
            recipeDisplay.text += recipe + "\n";
        }
        anim.SetBool("isOpen", true);
    }

    public void ClosePlantRecipesUI()
    {
        anim.SetBool("isOpen", false);
        recipeDisplay.text = null;
    }
}
