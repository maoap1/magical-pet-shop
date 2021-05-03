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

    private InventoryUI inventory;
    private RecipeProgress recipeProgress;

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
    }

    public void getRecipeInfo()
    {
        if (inventory.recipeInfo != null && recipeProgress != null)
        {
            inventory.recipeInfo.Open(recipeProgress);
        }
    }
}
