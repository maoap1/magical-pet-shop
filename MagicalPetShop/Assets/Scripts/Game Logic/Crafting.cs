using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Crafting
{
<<<<<<< HEAD
=======
<<<<<<< Updated upstream
    /// TODO
    /// tests if crafting can be started
    //public static bool CanStartCrafting(Recipe recipe) { }
    // try to start crafting
    //public static bool StartCrafting(Recipe recipe) { }
    // Try to finish crafting currently going on
=======
>>>>>>> parent of 24a2026 (Revert "fixed some issues")
    public static bool CanStartCrafting(RecipeProgress recipe) {
        Cost cost;
        cost.money = 0;
        cost.resources = recipe.costEssences;
        cost.artifacts = recipe.costArtifacts;
        cost.animals = recipe.costAnimals;
        return Inventory.HasInInventory(cost) && PlayerState.THIS.crafting.Count < PlayerState.THIS.craftingSlots;
    }

    public static bool StartCraftingSafe(RecipeProgress recipe) {
        CraftedAnimal ca = new CraftedAnimal();
        ca.fillRate = 0;
        ca.animal = recipe.animal;
        ca.rarity = randomImproveRarity(recipe.rarity);
        ca.duration = recipe.duration;
        ca.recipe = true;
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
            //recipe.animalProduced();
        }
        return result;
    }

    public static bool StartCrafting(RecipeProgress recipe) {
        CraftedAnimal ca = new CraftedAnimal();
        ca.fillRate = 0;
        ca.animal = recipe.animal;
        ca.rarity = randomImproveRarity(recipe.rarity);
        ca.duration = recipe.duration;
        ca.recipe = true;
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
            //recipe.animalProduced();
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
        if (artifactCost.count != 0)
        {
            cost.artifacts.Add(artifactCost);
        }
        cost.animals = new List<InventoryAnimal>();
        cost.animals.Add(animalCost);

        return Inventory.HasInInventory(cost) && PlayerState.THIS.crafting.Count < PlayerState.THIS.craftingSlots;
    }

    public static void StartMerging(InventoryAnimal animal)
    {
        RarityMergingSettings mergingCost = GameLogic.THIS.mergingSettings.mergingLevels[animal.animal.level].rarityMergingSettings[(int)animal.rarity - 1];
        CraftedAnimal ca = new CraftedAnimal();
        ca.fillRate = 0;
        ca.animal = animal.animal;
        ca.rarity = animal.rarity;
        ca.duration = mergingCost.duration;
        ca.recipe = false;


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
        if (artifactCost.count != 0)
        {
            cost.artifacts.Add(artifactCost);
        }
        cost.animals = new List<InventoryAnimal>();
        cost.animals.Add(animalCost);

        bool result = Inventory.TakeFromInventoryPrecise(cost);
        if (result)
        {
            PlayerState.THIS.crafting.Add(ca);
            PlayerState.THIS.Save();
        }
    }

    public static Rarity randomImproveRarity(Rarity input)
    {
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        float random = UnityEngine.Random.value;
        int increment = 0;
        PlayerState.THIS.unluckySeries[0]++;
        PlayerState.THIS.unluckySeries[1]++;
        PlayerState.THIS.unluckySeries[2]++;
        PlayerState.THIS.unluckySeries[3]++;
        if ((random < GameLogic.THIS.upgradeProbabilities[3]) || (PlayerState.THIS.unluckySeries[3] >= GameLogic.THIS.automaticUpgradeTresholds[3]))
        {
            increment = 4;
            PlayerState.THIS.unluckySeries[3] = 0;
        }
        else if ((random < GameLogic.THIS.upgradeProbabilities[2] + GameLogic.THIS.upgradeProbabilities[3]) || (PlayerState.THIS.unluckySeries[2] >= GameLogic.THIS.automaticUpgradeTresholds[2]))
        {
            increment = 3;
            PlayerState.THIS.unluckySeries[2] = 0;
        }
        else if ((random < GameLogic.THIS.upgradeProbabilities[1] + GameLogic.THIS.upgradeProbabilities[2] + GameLogic.THIS.upgradeProbabilities[3]) || (PlayerState.THIS.unluckySeries[1] >= GameLogic.THIS.automaticUpgradeTresholds[1]))
        {
            increment = 2;
            PlayerState.THIS.unluckySeries[1] = 0;
        }
        else if ((random < GameLogic.THIS.upgradeProbabilities[0] + GameLogic.THIS.upgradeProbabilities[1] + GameLogic.THIS.upgradeProbabilities[2] + GameLogic.THIS.upgradeProbabilities[3]) || (PlayerState.THIS.unluckySeries[0] >= GameLogic.THIS.automaticUpgradeTresholds[0]))
        {
            increment = 1;
            PlayerState.THIS.unluckySeries[0] = 0;
        }
        Rarity result = (Rarity)Mathf.Clamp(((int)input) + increment, 0, 4);
        return result;
    }
<<<<<<< HEAD
=======
>>>>>>> Stashed changes
>>>>>>> parent of 24a2026 (Revert "fixed some issues")
}

[Serializable]
public class CraftedAnimal
{
    public float fillRate;
    public Rarity rarity;
    public float duration;
<<<<<<< HEAD
    public Animal animal;
    public bool recipe;
=======
<<<<<<< Updated upstream
=======
    public Animal animal;
    public bool recipe;
>>>>>>> Stashed changes
>>>>>>> parent of 24a2026 (Revert "fixed some issues")
}
