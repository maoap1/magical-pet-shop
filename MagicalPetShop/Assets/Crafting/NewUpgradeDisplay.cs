using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AppearHideComponent))]
public class NewUpgradeDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI animalName;
    public Image image;
    public TMPro.TextMeshProUGUI text;
    public TMPro.TextMeshProUGUI rarityText;

    public void Open(RecipeProgress rp, RecipeLevel level)
    {
        text.gameObject.TweenAwareDisable();
        rarityText.gameObject.TweenAwareDisable();

        animalName.text = rp.animal.name;

        switch (level.upgradeType)
        {
            case RecipeUpgradeType.changeRarity:
                image.sprite = GameGraphics.THIS.changeRarity;
                rarityText.text = level.newRarity.ToString("G");
                rarityText.color = GameGraphics.THIS.getRarityColor(level.newRarity);
                rarityText.gameObject.TweenAwareEnable();
                break;
            case RecipeUpgradeType.increaseValue:
                image.sprite = GameGraphics.THIS.money;
                text.text = "+" + level.valueIncrease.ToString() + "%";
                text.gameObject.TweenAwareEnable();
                break;
            case RecipeUpgradeType.decreaseAnimals:
                image.sprite = level.costAnimalDecrease.animal.artwork;
                text.text = "-" + level.costAnimalDecrease.count;
                text.gameObject.TweenAwareEnable();
                break;
            case RecipeUpgradeType.decreaseArtifacts:
                image.sprite = level.costArtifactDecrease.artifact.artwork;
                text.text = "-" + level.costArtifactDecrease.count;
                text.gameObject.TweenAwareEnable();
                break;
            case RecipeUpgradeType.decreaseEssences:
                image.sprite = level.costEssenceDecrease.essence.icon;
                text.text = "-" + level.costEssenceDecrease.amount;
                text.gameObject.TweenAwareEnable();
                break;
            case RecipeUpgradeType.decreaseDuration:
                image.sprite = GameGraphics.THIS.getUpgradeSprite(RecipeUpgradeType.decreaseDuration);
                text.text = "-" + level.durationDecrease.ToString() + "%";
                text.gameObject.TweenAwareEnable();
                break;
        }
        GetComponent<AppearHideComponent>().Do();
        FindObjectOfType<AudioManager>().Play(SoundType.RecipeUpgrade);
    }

    public void Close()
    {
        GetComponent<AppearHideComponent>().Revert();
    }
}
