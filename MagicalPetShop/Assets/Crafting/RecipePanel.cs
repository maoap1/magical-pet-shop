using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipePanel : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI label;
    public GameObject costPanel;
    public GameObject costPartPrefab;
    public Button infoButton;
    public ProgressBar recipeProgress;
    [HideInInspector]
    public RecipeProgress recipe;

    public void UpdateInfo()
    {
        image.sprite = recipe.recipe.animal.artwork;
        label.text = recipe.recipe.animal.name;
        foreach (Transform child in costPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (EssenceAmount e in recipe.costEssences)
        {
            GameObject costPart = Instantiate(costPartPrefab, costPanel.transform);
            costPart.GetComponent<CostPartRecipe>().sprite = e.essence.icon;
            costPart.GetComponent<CostPartRecipe>().cost = e.amount;
        }
        foreach (InventoryArtifact a in recipe.costArtifacts)
        {
            GameObject costPart = Instantiate(costPartPrefab, costPanel.transform);
            costPart.GetComponent<CostPartRecipe>().sprite = a.artifact.artwork;
            costPart.GetComponent<CostPartRecipe>().cost = a.count;
        }
        foreach (InventoryAnimal a in recipe.costAnimals)
        {
            GameObject costPart = Instantiate(costPartPrefab, costPanel.transform);
            costPart.GetComponent<CostPartRecipe>().sprite = a.animal.artwork;
            costPart.GetComponent<CostPartRecipe>().cost = a.count;
        }

        if (recipe.progress == -1)
        {
            recipeProgress.gameObject.SetActive(false);
        }
        else {
            recipeProgress.fillRate = recipe.progress;
        }
    }
}
