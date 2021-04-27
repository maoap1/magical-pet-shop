using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergingPanel : MonoBehaviour
{
    public MergingSelection mergingPanel;
    public MergingImage image;
    public GameObject costPanel;
    public GameObject costPartPrefab;
    public TMPro.TextMeshProUGUI quality;
    public ResourceCost time;

    public InventoryAnimal animal;

    private float updateTime;

    public void UpdateInfo()
    {
        image.animal = animal;
        image.GetComponent<Image>().sprite = animal.animal.artwork;
        quality.text = animal.rarity.ToString("G");
        RarityMergingSettings mergingCost = GameLogic.THIS.mergingSettings.mergingLevels[animal.animal.level].rarityMergingSettings[(int)animal.rarity - 1];
        foreach (Transform child in costPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        GameObject animalCostDisplay = Instantiate(costPartPrefab, costPanel.transform);
        InventoryAnimal animalCost = new InventoryAnimal();
        animalCost.animal = animal.animal;
        animalCost.count = 2;
        animalCost.rarity = animal.rarity - 1;
        animalCostDisplay.GetComponent<ResourceCost>().SetCost(animalCost);
        LayoutRebuilder.ForceRebuildLayoutImmediate(animalCostDisplay.GetComponent<RectTransform>());

        if (mergingCost.artifactCost != 0)
        {
            GameObject artifactCostDisplay = Instantiate(costPartPrefab, costPanel.transform);
            InventoryArtifact artifactCost = new InventoryArtifact();
            artifactCost.artifact = animal.animal.associatedArtifact;
            artifactCost.count = mergingCost.artifactCost;
            artifactCostDisplay.GetComponent<ResourceCost>().SetCost(artifactCost);
            LayoutRebuilder.ForceRebuildLayoutImmediate(artifactCostDisplay.GetComponent<RectTransform>());
        }

        time.SetCostTime(mergingCost.duration);
        LayoutRebuilder.ForceRebuildLayoutImmediate(time.GetComponent<RectTransform>());
    }
}
