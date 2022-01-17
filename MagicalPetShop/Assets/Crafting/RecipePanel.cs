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
    public GameObject isNew;
    public GameObject ongoingCount;
    public TextMeshProUGUI ongoingCountText;

    public RecipeProgress recipe;

    private float updateTime;

    public void UpdateInfo()
    {
        if (recipe.newRecipe)
        {
            isNew.SetActive(true);
        }
        else
        {
            isNew.SetActive(false);
        }
        recipe.newRecipe = false;
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
            this.gameObject.GetComponent<Image>().color = UIPalette.THIS.GetColor(PaletteColor.HighlightLight);
        }
        else {
            recipeProgress.gameObject.SetActive(true);
            //upgrade.gameObject.SetActive(true);
            upgrade.color = Color.white;
            switch (recipe.nextUpgradeType)
            {
                case RecipeUpgradeType.changeRarity:
                    upgrade.sprite = GameGraphics.THIS.changeRarity;
                    break;
                case RecipeUpgradeType.increaseValue:
                    upgrade.sprite = GameGraphics.THIS.money;
                    break;
                case RecipeUpgradeType.decreaseAnimals:
                    upgrade.sprite = recipe.recipe.recipeLevels[recipe.level+1].costAnimalDecrease.animal.artwork;
                    break;
                case RecipeUpgradeType.decreaseArtifacts:
                    upgrade.sprite = recipe.recipe.recipeLevels[recipe.level+1].costArtifactDecrease.artifact.artwork;
                    break;
                case RecipeUpgradeType.decreaseEssences:
                    upgrade.sprite = recipe.recipe.recipeLevels[recipe.level+1].costEssenceDecrease.essence.icon;
                    break;
                case RecipeUpgradeType.decreaseDuration:
                    upgrade.sprite = GameGraphics.THIS.getUpgradeSprite(RecipeUpgradeType.decreaseDuration);
                    break;
                case RecipeUpgradeType.unlockRecipe:
                    upgrade.sprite = recipe.recipe.recipeLevels[recipe.level+1].unlockedRecipe.animal.artwork;
                    upgrade.color = Color.black;
                    break;
            }
            recipeProgress.fillRate = recipe.progress;
        }
        // Get number of ongoing recipes, if positive, show it
        int ongoing = 0;
        foreach (CraftedAnimal craftedAnimal in PlayerState.THIS.crafting) {
            if (craftedAnimal.animal == this.recipe.animal) ++ongoing;
        }
        if (ongoing > 0) {
            this.ongoingCountText.text = ongoing.ToString();
            this.ongoingCount.SetActive(true);
        } else {
            this.ongoingCount.SetActive(false);
        }
    }

    public void DisplayRecipeInfo()
    {
        recipesPanel.recipeInfo.tmpHidden.Add(recipesPanel.mergingButton.gameObject);
        recipesPanel.recipeInfo.tmpHidden.Add(recipesPanel.gameObject);
        recipesPanel.recipeInfo.Open(recipe);
    }
}
