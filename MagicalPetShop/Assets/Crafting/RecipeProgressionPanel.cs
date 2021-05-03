using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecipeProgressionPanel : MonoBehaviour
{
    public ProgressBar progressBar;
    public Image image;
    public TextMeshProUGUI text;
    public TextMeshProUGUI treshold;

    public void Display(RecipeProgress rp, int level)
    {
        if (rp.level >= level)
        {
            progressBar.fillRate = 1;
        }
        else if (rp.level == level-1)
        {
            progressBar.fillRate = rp.progress;
        }
        else
        {
            progressBar.fillRate = 0;
        }
        treshold.text = rp.recipe.recipeLevels[level].treshold.ToString();
        switch (rp.recipe.recipeLevels[level].upgradeType)
        {
            case RecipeUpgradeType.changeRarity:
                image.sprite = GameGraphics.THIS.changeRarity;
                text.text = rp.recipe.recipeLevels[level].newRarity.ToString("G");
                break;
            case RecipeUpgradeType.increaseValue:
                image.sprite = GameGraphics.THIS.money;
                text.text = "+" + rp.recipe.recipeLevels[level].valueIncrease.ToString() + "%";
                break;
            case RecipeUpgradeType.decreaseAnimals:
                image.sprite = rp.recipe.recipeLevels[level].costAnimalDecrease.animal.artwork;
                text.text = "-" + rp.recipe.recipeLevels[level].costAnimalDecrease.count;
                break;
            case RecipeUpgradeType.decreaseArtifacts:
                image.sprite = rp.recipe.recipeLevels[level].costArtifactDecrease.artifact.artwork;
                text.text = "-" + rp.recipe.recipeLevels[level].costArtifactDecrease.count;
                break;
            case RecipeUpgradeType.decreaseEssences:
                image.sprite = rp.recipe.recipeLevels[level].costEssenceDecrease.essence.icon;
                text.text = "-" + rp.recipe.recipeLevels[level].costEssenceDecrease.amount;
                break;
            case RecipeUpgradeType.decreaseDuration:
                image.sprite = GameGraphics.THIS.getUpgradeSprite(RecipeUpgradeType.decreaseDuration);
                text.text = "-" + rp.recipe.recipeLevels[level].durationDecrease.ToString() + "%";
                break;
            case RecipeUpgradeType.unlockRecipe:
                text.gameObject.SetActive(false);
                image.rectTransform.sizeDelta = new Vector2(180, 180);
                image.sprite = rp.recipe.recipeLevels[level].unlockedRecipe.animal.artwork;
                if (rp.level < level)
                {
                    image.color = Color.black;
                }
                break;
        }
    }
}
