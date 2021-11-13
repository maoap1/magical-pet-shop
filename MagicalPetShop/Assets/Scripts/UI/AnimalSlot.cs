using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalSlot : MonoBehaviour
{
    [SerializeField]
    private Text name;
    [SerializeField]
    private Text value;
    [SerializeField]
    public Image image;
    [SerializeField]
    private Text count;
    [SerializeField]
    private Text quality;
    [SerializeField]
    private Button info;
    [SerializeField]
    public Button Lock;
    public Sprite lockedImage;
    public Sprite unlockedImage;

    private InventoryUI inventory;
    private RecipeProgress recipeProgress;
    private InventoryAnimal inventoryAnimal;

    public void SetAnimal(InventoryAnimal animal, InventoryUI inventory) {
        this.image.material = new Material(this.image.material);
        this.inventory = inventory;
        this.name.text = animal.animal.name;
        this.value.text = ((int)(animal.animal.value * GameLogic.THIS.getRarityMultiplier(animal.rarity) * PlayerState.THIS.recipes.Find(r => r.animal == animal.animal).costMultiplier)).ToString();
        this.image.sprite = animal.animal.artwork;
        this.image.material.SetColor("_Color", GameGraphics.THIS.getRarityColor(animal.rarity));
        this.image.material.SetTexture("_BloomTex", animal.animal.bloomSprite.texture);
        this.count.text = animal.count.ToString();
        this.quality.text = animal.rarity.ToString("G");
        this.recipeProgress = PlayerState.THIS.recipes.Find(r => r.recipe.animal == animal.animal);
        this.inventoryAnimal = PlayerState.THIS.animals.Find(a => a == animal); ;
        if (animal.rarity == this.recipeProgress.rarity) { inventoryAnimal.locked = false; Lock.gameObject.SetActive(false); }
        if (inventoryAnimal.locked)
        {
            Lock.image.sprite = lockedImage;
        }
        else
        {
            Lock.image.sprite = unlockedImage;
        }
    }

    public void getRecipeInfo()
    {
        if (inventory.recipeInfo != null && recipeProgress != null)
        {
            inventory.recipeInfo.Open(recipeProgress);
        }
    }

    public void toggleLock()
    {
        inventoryAnimal.locked = !inventoryAnimal.locked;
        if (inventoryAnimal.locked)
        {
            Lock.image.sprite = lockedImage;
        }
        else
        {
            Lock.image.sprite = unlockedImage;
        }
    }
}
