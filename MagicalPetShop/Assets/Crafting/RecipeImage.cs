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
    public bool canCraft;
    private float updateTime = 0;
    public void Clicked()
    {
        if (canCraft && PlayerState.THIS.crafting.Count < 5)
        {
            CraftedAnimal ca = new CraftedAnimal();
            ca.fillRate = 0;
            ca.recipe = recipe;
            ca.rarity = recipe.rarity;
            ca.duration = recipe.duration;
            Cost cost;
            cost.money = 0;
            cost.resources = recipe.costEssences;
            cost.artifacts = recipe.costArtifacts;
            cost.animals = recipe.costAnimals;
            Inventory.TakeFromInventory(cost);
            PlayerState.THIS.crafting.Add(ca);
            int recipesBefore = PlayerState.THIS.recipes.Count;
            recipe.animalProduced();
            if (PlayerState.THIS.recipes.Count > recipesBefore)
            {
                recipePanel.recipesPanel.defaultRecipeCategory.Display();
            }
            recipePanel.UpdateInfo();
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
        Cost cost;
        cost.money = 0;
        cost.resources = recipe.costEssences;
        cost.artifacts = recipe.costArtifacts;
        cost.animals = recipe.costAnimals;
        updateTime = Time.time;
        if (Inventory.HasInInventory(cost) && PlayerState.THIS.crafting.Count < 5)
        {
            gameObject.GetComponent<Image>().material.SetFloat("_GrayscaleAmount", 0);
            canCraft = true;
        }
        else
        {
            gameObject.GetComponent<Image>().material.SetFloat("_GrayscaleAmount", 1);
            canCraft = false;
        }
    }
}
