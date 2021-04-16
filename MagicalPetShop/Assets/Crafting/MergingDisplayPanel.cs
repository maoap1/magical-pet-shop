using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergingDisplayPanel : MonoBehaviour
{
    public GameObject mergingPanelPrefab;
    public MergingSelection mergingPanel;
    public void Display(List<InventoryAnimal> animalsList)
    {
        foreach (InventoryAnimal ia in animalsList)
        {
            InventoryAnimal inventoryAnimalMerging = new InventoryAnimal();
            inventoryAnimalMerging.animal = ia.animal;
            inventoryAnimalMerging.count = 1;
            inventoryAnimalMerging.rarity = ia.rarity + 1;
            MergingPanel mergingPanel = Instantiate(mergingPanelPrefab, transform).GetComponent<MergingPanel>();
            mergingPanel.animal = inventoryAnimalMerging;
            mergingPanel.mergingPanel = this.mergingPanel;
            mergingPanel.UpdateInfo();
        }
    }
}
