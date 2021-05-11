using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftedAnimalDisplay : MonoBehaviour
{
    public CraftedAnimal craftedAnimal;
    public ProgressBar progressRing;
    public GameObject readyMessage;
    public Image animalImage;
    private bool finished;
    public void Start()
    {
        readyMessage.SetActive(true);
        progressRing.gameObject.SetActive(true);
        finished = false;
        animalImage.sprite = craftedAnimal.animal.artwork;
        if (!PlayerState.THIS.crafting.Contains(craftedAnimal))
        {
            PlayerState.THIS.crafting.Add(craftedAnimal);
        }
        Update();
    }
    public void Update()
    {
        if (!finished)
        {
            if (craftedAnimal.fillRate >= 1)
            {
                finished = true;
                progressRing.gameObject.SetActive(false);
                readyMessage.SetActive(true);
            }
            else
            {
                readyMessage.SetActive(false);
                progressRing.fillRate = craftedAnimal.fillRate;
            }
        }
        if (PlayerState.THIS.crafting.Find(x => x == craftedAnimal) == null)
        {
            Destroy(this.gameObject);
        }
    }
    public void OnPointerClicked()
    {
        if (finished)
        {
            InventoryAnimal ia = new InventoryAnimal();
            ia.animal = craftedAnimal.animal;
            ia.count = 1;
            ia.rarity = craftedAnimal.rarity;
            Inventory.AddToInventory(ia);
            PlayerState.THIS.crafting.Remove(craftedAnimal);
            PlayerState.THIS.Save();
            if (craftedAnimal.isUpgraded)
            {
                HigherRarityCrafted newRecipeDisplay = Resources.FindObjectsOfTypeAll<HigherRarityCrafted>()[0];
                newRecipeDisplay.Open(PlayerState.THIS.recipes.Find(r => r.animal == craftedAnimal.animal), craftedAnimal.rarity);
            }
            else if (craftedAnimal.isRecipe)
            {
                PlayerState.THIS.recipes.Find(r => r.animal == craftedAnimal.animal).animalProduced();
            }
            FindObjectOfType<AudioManager>().Play(SoundType.Crafting);
            Destroy(this.gameObject);
        }
    }
}
