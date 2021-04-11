using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingInfo : MonoBehaviour
{
    public GameObject craftedAnimalPrefab;
    public float updateTime;

    void Update()
    {
        if (Time.time - updateTime > 0.1)
        {
            updateTime = Time.time;
            List<CraftedAnimalDisplay> displays = new List<CraftedAnimalDisplay>(gameObject.GetComponentsInChildren<CraftedAnimalDisplay>());
            foreach (CraftedAnimal ca in PlayerState.THIS.crafting)
            {
                if (displays.Find(x => x.craftedAnimal==ca) == null)
                {
                    CraftedAnimalDisplay display = Instantiate(craftedAnimalPrefab, this.transform).GetComponent<CraftedAnimalDisplay>();
                    display.craftedAnimal = ca;
                }
            }
        }
    }
}
