using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipePanel : MonoBehaviour
{
    public RecipeSelection recipesPanel;
    public RecipeImage image;
    public TextMeshProUGUI label;
    public GameObject costPanel;
    public GameObject costPartPrefab;
    public Button infoButton;
    public ProgressBar recipeProgress;
    public Image upgrade;
    [HideInInspector]
    public RecipeProgress recipe;

    private float updateTime;

    public void UpdateInfo()
    {
        image.recipe = recipe;
        image.GetComponent<Image>().sprite = recipe.recipe.animal.artwork;
        label.text = recipe.recipe.animal.name;
        foreach (Transform child in costPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (EssenceAmount e in recipe.costEssences)
        {
            GameObject costPart = Instantiate(costPartPrefab, costPanel.transform);
            costPart.GetComponent<ResourceCost>().SetCost(e);
        }
        foreach (InventoryArtifact a in recipe.costArtifacts)
        {
            GameObject costPart = Instantiate(costPartPrefab, costPanel.transform);
            costPart.GetComponent<ResourceCost>().SetCost(a);
        }
        foreach (InventoryAnimal a in recipe.costAnimals)
        {
            GameObject costPart = Instantiate(costPartPrefab, costPanel.transform);
            costPart.GetComponent<ResourceCost>().SetCost(a);
        }

        if (recipe.progress == -1)
        {
            recipeProgress.gameObject.SetActive(false);
            upgrade.gameObject.SetActive(false);
        }
        else {
            recipeProgress.gameObject.SetActive(true);
            upgrade.gameObject.SetActive(true);
            upgrade.sprite = GameGraphics.THIS.getUpgradeSprite((RecipeUpgradeType)recipe.nextUpgradeType);
            recipeProgress.fillRate = recipe.progress;
        }
    }

    public void DisplayRecipeInfo()
    {
        recipesPanel.recipeInfo.tmpHidden.Add(recipesPanel.gameObject);
        recipesPanel.recipeInfo.Open(recipe);
    }
}
