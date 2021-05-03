using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeImage : MonoBehaviour
{
    public RecipePanel recipePanel;
    [HideInInspector]
    public RecipeProgress recipe;
    private float updateTime = 0;
    public void Clicked()
    {
        if (Crafting.CanStartCrafting(recipe))
        {
            int recipesBefore = PlayerState.THIS.recipes.Count;
            if (Crafting.StartCraftingSafe(recipe))
            {
                FindObjectOfType<AudioManager>().Play(AudioType.Splash);
                if (PlayerState.THIS.recipes.Count > recipesBefore)
                {
                    recipePanel.recipesPanel.defaultRecipeCategory.Display();
                }
                recipePanel.UpdateInfo();
            }
            else
            {
                recipePanel.recipesPanel.confirmationPanel.Open(recipe);
                if (PlayerState.THIS.recipes.Count > recipesBefore)
                {
                    recipePanel.recipesPanel.defaultRecipeCategory.Display();
                }
                recipePanel.UpdateInfo();
            }
        }
    }
    private void Update()
    {
        if (recipe != null && Time.time - updateTime > 0.5)
        {
            updateGrayscale();
        }
    }

    private void Start()
    {
        gameObject.GetComponent<Image>().material = new Material(gameObject.GetComponent<Image>().material);
    }

    public void updateGrayscale()
    {
        updateTime = Time.time;
        if (Crafting.CanStartCrafting(recipe))
        {
            gameObject.GetComponent<Image>().material.SetFloat("_GrayscaleAmount", 0);
        }
        else
        {
            gameObject.GetComponent<Image>().material.SetFloat("_GrayscaleAmount", 1);
        }
    }
}
