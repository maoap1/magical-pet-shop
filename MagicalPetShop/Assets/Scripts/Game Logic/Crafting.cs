using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Analytics;

public static class Crafting
{
    public static bool randomImproveQuality = true;
    public static bool CanStartCrafting(RecipeProgress recipe) {
        Cost cost;
        cost.money = 0;
        cost.resources = recipe.costEssences;
        cost.artifacts = recipe.costArtifacts;
        cost.animals = recipe.costAnimals;
        return Inventory.HasInInventory(cost) && PlayerState.THIS.crafting.Count < PlayerState.THIS.craftingSlots;
    }

    private static CraftedAnimal CreateCraftedAnimal(RecipeProgress recipe)
    {
        CraftedAnimal ca = new CraftedAnimal();
        ca.fillRate = 0;
        ca.animal = recipe.animal;
        ca.rarity = randomImproveRarity(recipe.rarity);
        ca.duration = recipe.duration;
        ca.isRecipe = true;
        ca.recipe = recipe.recipe;
        ca.animalsProduced = recipe.animalsProduced;
        return ca;
    }
    private static Cost CreateCost(RecipeProgress recipe)
    {
        Cost cost;
        cost.money = 0;
        cost.resources = recipe.costEssences;
        cost.artifacts = recipe.costArtifacts;
        cost.animals = recipe.costAnimals;
        return cost;
    }

    // Start crafting with the exact ingredients
    public static bool StartCraftingSafe(RecipeProgress recipe) 
    {
        return StartCraftingImpl(recipe, Inventory.TakeFromInventoryPrecise);
    }

    // Start crafting even with higher quality ingredints (comes from confirmation panel)
    public static bool StartCrafting(RecipeProgress recipe)
    {
        return StartCraftingImpl(recipe, Inventory.TakeFromInventory);
    }

    private static bool StartCraftingImpl(RecipeProgress recipe, System.Func<Cost, bool> takeFromInventory)
    {
        var ca = CreateCraftedAnimal(recipe);
        var cost = CreateCost(recipe);

        bool result = takeFromInventory(cost);
        if (result)
        {
            PlayerState.THIS.crafting.Add(ca);
            PlayerState.THIS.Save();
            Utils.FindObject<CraftingInfo>()[0].AddAnimal(ca);
            Analytics.LogEvent("crafting_started", new Parameter("animal", ca.animal.name), new Parameter("rarity", ca.rarity.ToString()));
            // TODO: Here update the Crafting Info
            //recipe.animalProduced();
        }
        return result;
    }

    public static bool CanStartMerging(InventoryAnimal animal)
    {
        RarityMergingSettings mergingCost = GameLogic.THIS.mergingSettings.mergingLevels[animal.animal.level-1].rarityMergingSettings[(int)animal.rarity - 1];
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
        RarityMergingSettings mergingCost = GameLogic.THIS.mergingSettings.mergingLevels[animal.animal.level-1].rarityMergingSettings[(int)animal.rarity - 1];
        CraftedAnimal ca = new CraftedAnimal();
        ca.fillRate = 0;
        ca.animal = animal.animal;
        ca.rarity = animal.rarity;
        ca.duration = mergingCost.duration;
        ca.isRecipe = false;


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
            Utils.FindObject<CraftingInfo>()[0].AddAnimal(ca);
            Analytics.LogEvent("merging_started", new Parameter("animal", ca.animal.name), new Parameter("rarity", ca.rarity.ToString()));
        }
    }

    public static Rarity randomImproveRarity(Rarity input)
    {
        if (!randomImproveQuality)
        {
            return input;
        }
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
}

[Serializable]
public class CraftedAnimal
{
    public float fillRate;
    public Rarity rarity;
    public float duration;
    public Animal animal;
    public bool isRecipe;
    [SerializeReference]
    public Recipe recipe;
    public int animalsProduced;

    public int RemainingSeconds => (int)((1 - fillRate) * duration);

    public bool isUpgraded
    {
        get
        {
            if (!isRecipe) { return false; }
            else
            {
                Rarity expectedRarity = recipe.getRarity(animalsProduced);
                if (rarity > expectedRarity)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
