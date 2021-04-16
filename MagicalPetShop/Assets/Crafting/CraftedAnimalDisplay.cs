using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftedAnimalDisplay : MonoBehaviour, IPointerClickHandler
{
    public CraftedAnimal craftedAnimal;
    public ProgressBar progressRing;
    public GameObject readyMessage;
    public Image animalImage;
    private bool finished;
    public void Start()
    {
        readyMessage.SetActive(false);
        progressRing.gameObject.SetActive(true);
        finished = false;
        animalImage.sprite = craftedAnimal.animal.artwork;
        if (!PlayerState.THIS.crafting.Contains(craftedAnimal))
        {
            PlayerState.THIS.crafting.Add(craftedAnimal);
        }
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
                progressRing.fillRate = craftedAnimal.fillRate;
            }
        }
        if (PlayerState.THIS.crafting.Find(x => x == craftedAnimal) == null)
        {
            Destroy(this.gameObject);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
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
            Destroy(this.gameObject);
        }
    }
}
