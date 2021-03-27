using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeDisplayPanel : MonoBehaviour
{
    public GameObject recipePanelPrefab;
    public void Display(List<RecipeProgress> recipeList)
    {
        foreach (RecipeProgress rp in recipeList)
        {
            RecipePanel recipePanel = Instantiate(recipePanelPrefab, transform).GetComponent<RecipePanel>();
            recipePanel.recipe = rp;
            recipePanel.UpdateInfo();
        }
    }
}
