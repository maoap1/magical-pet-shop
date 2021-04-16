using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Crafting
{
    public static bool CanStartCrafting(RecipeProgress recipe) {
        Cost cost;
        cost.money = 0;
        cost.resources = recipe.costEssences;
        cost.artifacts = recipe.costArtifacts;
        cost.animals = recipe.costAnimals;
        return Inventory.HasInInventory(cost) && PlayerState.THIS.crafting.Count < 5;
    }

    public static bool StartCraftingSafe(RecipeProgress recipe) {
        CraftedAnimal ca = new CraftedAnimal();
        ca.fillRate = 0;
        ca.animal = recipe.animal;
        ca.rarity = recipe.rarity;
        ca.duration = recipe.duration;
        Cost cost;
        cost.money = 0;
        cost.resources = recipe.costEssences;
        cost.artifacts = recipe.costArtifacts;
        cost.animals = recipe.costAnimals;
        bool result = Inventory.TakeFromInventoryPrecise(cost);
        if (result)
        {
            PlayerState.THIS.crafting.Add(ca);
            PlayerState.THIS.Save();
            recipe.animalProduced();
        }
        return result;
    }

    public static bool StartCrafting(RecipeProgress recipe) {
        CraftedAnimal ca = new CraftedAnimal();
        ca.fillRate = 0;
        ca.animal = recipe.animal;
        ca.rarity = recipe.rarity;
        ca.duration = recipe.duration;
        Cost cost;
        cost.money = 0;
        cost.resources = recipe.costEssences;
        cost.artifacts = recipe.costArtifacts;
        cost.animals = recipe.costAnimals;
        bool result = Inventory.TakeFromInventory(cost);
        if (result)
        {
            PlayerState.THIS.crafting.Add(ca);
            PlayerState.THIS.Save();
            recipe.animalProduced();
        }
        return result;
    }

    public static bool CanStartMerging(InventoryAnimal animal)
    {
        RarityMergingSettings mergingCost = GameLogic.THIS.mergingSettings.mergingLevels[animal.animal.level].rarityMergingSettings[(int)animal.rarity - 1];
        InventoryAnimal animalCost = new InventoryAnimal();
        animalCost.animal = animal.animal;
        animalCost.count = 2;
        animalCost.rarity = animal.rarity - 1;

        InventoryArtifact artifactCost = new InventoryArtifact();
        artifactCost.artifact = animal.animal.associatedArtifact;
        artifactCost.count = mergingCost.artifactCost;

        Cost cost;
        cost.money = 0;
        cost.resources = new List<EssenceAmount>();
        cost.artifacts = new List<InventoryArtifact>();
        cost.artifacts.Add(artifactCost);
        cost.animals = new List<InventoryAnimal>();
        cost.animals.Add(animalCost);

        return Inventory.HasInInventory(cost) && PlayerState.THIS.crafting.Count < 5;
    }

    public static void StartMerging(InventoryAnimal animal)
    {
        RarityMergingSettings mergingCost = GameLogic.THIS.mergingSettings.mergingLevels[animal.animal.level].rarityMergingSettings[(int)animal.rarity - 1];
        CraftedAnimal ca = new CraftedAnimal();
        ca.fillRate = 0;
        ca.animal = animal.animal;
        ca.rarity = animal.rarity;
        ca.duration = mergingCost.duration;


        InventoryAnimal animalCost = new InventoryAnimal();
        animalCost.animal = animal.animal;
        animalCost.count = 2;
        animalCost.rarity = animal.rarity - 1;

        InventoryArtifact artifactCost = new InventoryArtifact();
        artifactCost.artifact = animal.animal.associatedArtifact;
        artifactCost.count = mergingCost.artifactCost;

        Cost cost;
        cost.money = 0;
        cost.resources = new List<EssenceAmount>();
        cost.artifacts = new List<InventoryArtifact>();
        cost.artifacts.Add(artifactCost);
        cost.animals = new List<InventoryAnimal>();
        cost.animals.Add(animalCost);

        bool result = Inventory.TakeFromInventoryPrecise(cost);
        if (result)
        {
            PlayerState.THIS.crafting.Add(ca);
            PlayerState.THIS.Save();
        }
    }
}

[Serializable]
public class CraftedAnimal
{
    public float fillRate;
    public Rarity rarity;
    public float duration;
    public Animal animal;
}
