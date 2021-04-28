using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingInfo : MonoBehaviour
{
    public GameObject craftedAnimalPrefab;
    public CraftingUpgrade buyCraftingSlotObject;
    public float updateTime;

    void Update()
    {
        if (Time.time - updateTime > 1)
        {
            updateTime = Time.time;
            UpdateCrafting();
        }
    }

    public void UpdateCrafting()
    {
        foreach (Transform child in gameObject.transform)
        {
            if (child.GetComponent<CraftedAnimalDisplay>() != null)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        PlayerState.THIS.crafting.Sort((a1, a2) => (a1.duration * (1 - a1.fillRate)).CompareTo((a2.duration * (1 - a2.fillRate))));
        foreach (CraftedAnimal ca in PlayerState.THIS.crafting)
        {

            CraftedAnimalDisplay display = Instantiate(craftedAnimalPrefab, this.transform).GetComponent<CraftedAnimalDisplay>();
            display.craftedAnimal = ca;
            display.Update();
        }

        if (PlayerState.THIS.craftingSlots < 5 && PlayerState.THIS.crafting.Count == PlayerState.THIS.craftingSlots && GameLogic.THIS.craftingSlotUpgrades[PlayerState.THIS.craftingSlots - 1].level <= PlayerState.THIS.level)
        {
            buyCraftingSlotObject.gameObject.SetActive(true);
        }
        else
        {
            buyCraftingSlotObject.gameObject.SetActive(false);
        }
        buyCraftingSlotObject.transform.SetAsLastSibling();
    }
}
