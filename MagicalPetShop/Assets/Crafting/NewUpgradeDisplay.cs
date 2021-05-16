using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewUpgradeDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI animalName;
    public Image image;
    public TMPro.TextMeshProUGUI text;

    public List<GameObject> objectsToAppear;
    public List<GameObject> objectsToHide;

    public void Open(RecipeProgress rp, RecipeLevel level)
    {
        animalName.text = rp.animal.name;

        switch (level.upgradeType)
        {
            case RecipeUpgradeType.changeRarity:
                image.sprite = GameGraphics.THIS.changeRarity;
                text.text = level.newRarity.ToString("G");
                break;
            case RecipeUpgradeType.increaseValue:
                image.sprite = GameGraphics.THIS.money;
                text.text = "+" + level.valueIncrease.ToString() + "%";
                break;
            case RecipeUpgradeType.decreaseAnimals:
                image.sprite = level.costAnimalDecrease.animal.artwork;
                text.text = "-" + level.costAnimalDecrease.count;
                break;
            case RecipeUpgradeType.decreaseArtifacts:
                image.sprite = level.costArtifactDecrease.artifact.artwork;
                text.text = "-" + level.costArtifactDecrease.count;
                break;
            case RecipeUpgradeType.decreaseEssences:
                image.sprite = level.costEssenceDecrease.essence.icon;
                text.text = "-" + level.costEssenceDecrease.amount;
                break;
            case RecipeUpgradeType.decreaseDuration:
                image.sprite = GameGraphics.THIS.getUpgradeSprite(RecipeUpgradeType.decreaseDuration);
                text.text = "-" + level.durationDecrease.ToString() + "%";
                break;
        }
        foreach (GameObject g in objectsToAppear) {
            g.SetActive(true);
        }
        this.gameObject.SetActive(true);
        foreach (GameObject g in objectsToHide) {
            g.SetActive(false);
        }
        FindObjectOfType<AudioManager>().Play(SoundType.Success);
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
        foreach (GameObject g in objectsToAppear) {
            g.SetActive(false);
        }
        foreach (GameObject g in objectsToHide) {
            g.SetActive(true);
        }
    }
}
