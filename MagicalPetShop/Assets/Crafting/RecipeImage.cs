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
                FindObjectOfType<AudioManager>().Play(SoundType.Splash);
                if (PlayerState.THIS.recipes.Count > recipesBefore)
                {
                    recipePanel.recipesPanel.defaultRecipeCategory.Display();
                }
                recipePanel.UpdateInfo();
                // close the crafting menu, if all slots are full
                if (PlayerState.THIS.crafting.Count == PlayerState.THIS.craftingSlots)
                    GameObject.FindObjectOfType<RecipeSelection>().Close();
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
            gameObject.GetComponent<Image>().materialForRendering.SetFloat("_GrayscaleAmount", 0);
            recipePanel.GetComponent<Button>().interactable = true;
            if (recipe.progress == -1)
            {
                recipePanel.gameObject.GetComponent<Image>().color = new Color(130, 100, 0, 255);
            }
        }
        else
        {
            gameObject.GetComponent<Image>().materialForRendering.SetFloat("_GrayscaleAmount", 1);
            recipePanel.GetComponent<Button>().interactable = false;
            recipePanel.GetComponent<Image>().color = Color.white;
        }
    }
}
