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
<<<<<<< HEAD
=======
<<<<<<< Updated upstream
=======
>>>>>>> parent of 24a2026 (Revert "fixed some issues")
    [SerializeField]
    private Text quality;
    [SerializeField]
    private Button info;
<<<<<<< HEAD

    private InventoryUI inventory;
    private RecipeProgress recipeProgress;

    public void SetAnimal(InventoryAnimal animal, InventoryUI inventory) {
        this.inventory = inventory;
        this.name.text = animal.animal.name;
        this.value.text = ((int)(animal.animal.value * GameLogic.THIS.getRarityMultiplier(animal.rarity) * PlayerState.THIS.recipes.Find(r => r.animal == animal.animal).costMultiplier)).ToString();
        this.image.sprite = animal.animal.artwork;
        this.count.text = animal.count.ToString();
=======

    private InventoryUI inventory;
    private RecipeProgress recipeProgress;
>>>>>>> Stashed changes

    public void SetAnimal(InventoryAnimal animal, InventoryUI inventory) {
        this.inventory = inventory;
        this.name.text = animal.animal.name;
<<<<<<< Updated upstream
        this.value.text = animal.animal.value.ToString();
        this.image.sprite = animal.animal.artwork;
        this.count.text = animal.count.ToString();
=======
        this.value.text = ((int)(animal.animal.value * GameLogic.THIS.getRarityMultiplier(animal.rarity) * PlayerState.THIS.recipes.Find(r => r.animal == animal.animal).costMultiplier)).ToString();
        this.image.sprite = animal.animal.artwork;
        this.count.text = animal.count.ToString();
>>>>>>> parent of 24a2026 (Revert "fixed some issues")
        this.quality.text = animal.rarity.ToString("G");
        this.recipeProgress = PlayerState.THIS.recipes.Find(r => r.recipe.animal == animal.animal);
    }

    public void getRecipeInfo()
    {
        if (inventory.recipeInfo != null && recipeProgress != null)
        {
            inventory.recipeInfo.Open(recipeProgress);
        }
<<<<<<< HEAD
=======
>>>>>>> Stashed changes
>>>>>>> parent of 24a2026 (Revert "fixed some issues")
    }
}
