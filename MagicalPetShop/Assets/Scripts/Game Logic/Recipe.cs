using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Firebase.Analytics;

[CreateAssetMenu(fileName = "Recipe", menuName = "PetShop/Recipe")]
public class Recipe : ScriptableObject
{
    public Animal animal;
    public List<EssenceAmount> baseCostEssences;
    public List<InventoryArtifact> baseCostArtifacts;
    public List<InventoryAnimal> baseCostAnimals;
    public Rarity baseRarity;
    public int baseDuration;
    public List<RecipeLevel> recipeLevels;

    public int getCurrentLevel(int animalsProduced)
    {
        int result = -1;
        for (int i=0; i<recipeLevels.Count; i++)
        {
            if (animalsProduced >= recipeLevels[i].treshold)
            {
                result = i;
            }
        }
        return result;
    }

    public List<EssenceAmount> getEssences(int animalsProduced)
    {
        List<EssenceAmount> essences = new List<EssenceAmount>();
        foreach (EssenceAmount ea in baseCostEssences)
        {
            EssenceAmount eaCreated = new EssenceAmount();
            eaCreated.essence = ea.essence;
            eaCreated.amount = ea.amount;

            essences.Add(eaCreated);
        }
        for (int i = 0; i < recipeLevels.Count; i++)
        {
            if (animalsProduced >= recipeLevels[i].treshold && recipeLevels[i].upgradeType == RecipeUpgradeType.decreaseEssences)
            {
                EssenceAmount amount = recipeLevels[i].costEssenceDecrease;
                EssenceAmount changedAmount = essences.Find(x => x.essence == amount.essence);
                if (changedAmount != null)
                {
                    changedAmount.amount -= amount.amount;
                    if (changedAmount.amount <= 0)
                    {
                        essences.Remove(changedAmount);
                    }
                }
            }
        }
        return essences;
    }


    public List<InventoryArtifact> getArtifacts(int animalsProduced)
    {
        List<InventoryArtifact> artifacts = new List<InventoryArtifact>();
        foreach (InventoryArtifact ia in baseCostArtifacts)
        {
            InventoryArtifact iaCreated = new InventoryArtifact();
            iaCreated.artifact = ia.artifact;
            iaCreated.count = ia.count;

            artifacts.Add(iaCreated);
        }
        for (int i = 0; i < recipeLevels.Count; i++)
        {
            if (animalsProduced >= recipeLevels[i].treshold && recipeLevels[i].upgradeType == RecipeUpgradeType.decreaseArtifacts)
            {
                InventoryArtifact amount = recipeLevels[i].costArtifactDecrease;
                InventoryArtifact changedAmount = artifacts.Find(x => x.artifact == amount.artifact);
                if (changedAmount != null)
                {
                    changedAmount.count -= amount.count;
                    if (changedAmount.count <= 0)
                    {
                        artifacts.Remove(changedAmount);
                    }
                }
            }
        }
        return artifacts;
    }


    public List<InventoryAnimal> getAnimals(int animalsProduced)
    {
        List<InventoryAnimal> animals = new List<InventoryAnimal>();
        foreach (InventoryAnimal ia in baseCostAnimals)
        {
            InventoryAnimal iaCreated = new InventoryAnimal();
            iaCreated.animal = ia.animal;
            iaCreated.count = ia.count;

            animals.Add(iaCreated);
        }
        for (int i = 0; i < recipeLevels.Count; i++)
        {
            if (animalsProduced >= recipeLevels[i].treshold && recipeLevels[i].upgradeType == RecipeUpgradeType.decreaseAnimals)
            {
                InventoryAnimal amount = recipeLevels[i].costAnimalDecrease;             
                InventoryAnimal changedAmount = animals.Find(x => x.animal == amount.animal);
                if (changedAmount != null)
                {
                    changedAmount.count -= amount.count;
                    if (changedAmount.count <= 0)
                    {
                         animals.Remove(changedAmount);
                    }
                }
            }
        }
        return animals;
    }


    public int getDuration(int animalsProduced)
    {
        int duration = baseDuration;
        int durationDecrease = 0;
        for (int i = 0; i < recipeLevels.Count; i++)
        {
            if (animalsProduced >= recipeLevels[i].treshold && recipeLevels[i].upgradeType==RecipeUpgradeType.decreaseDuration)
            {
                durationDecrease += recipeLevels[i].durationDecrease;
            }
        }
        duration -= (int)(baseDuration * (float)durationDecrease / (float)100);
        return duration;
    }

    public float getCostMultiplier(int animalsProduced)
    {
        float multiplier = 1;
        for (int i = 0; i < recipeLevels.Count; i++)
        {
            if (animalsProduced >= recipeLevels[i].treshold && recipeLevels[i].upgradeType == RecipeUpgradeType.increaseValue)
            {
                multiplier += (float)recipeLevels[i].valueIncrease / (float)100;
            }
        }
        return multiplier;
    }

    public Rarity getRarity(int animalsProduced)
    {
        Rarity rarity = baseRarity;
        for (int i = 0; i < recipeLevels.Count; i++)
        {
            if (animalsProduced >= recipeLevels[i].treshold && recipeLevels[i].upgradeType == RecipeUpgradeType.changeRarity)
            {
                rarity = recipeLevels[i].newRarity;
            }
        }
        return rarity;
    }

    public Recipe getUnlockedRecipe(int animalsProduced)
    {
        int level = getCurrentLevel(animalsProduced);
        if (level >= 0 && recipeLevels[level].treshold == animalsProduced)
        {
            return recipeLevels[level].unlockedRecipe;
        }
        return null;
    }

    public RecipeLevel getUnlockedUpgrade(int animalsProduced)
    {
        int level = getCurrentLevel(animalsProduced);
        if (level >= 0 && recipeLevels[level].treshold == animalsProduced)
        {
            return recipeLevels[level];
        }
        return null;
    }
}

[System.Serializable]
public class RecipeLevel
{
    public int treshold;
    public RecipeUpgradeType upgradeType;
    [ConditionalEnumHide("upgradeType", 0)]
    public EssenceAmount costEssenceDecrease;
    [ConditionalEnumHide("upgradeType", 1)]
    public InventoryArtifact costArtifactDecrease;
    [ConditionalEnumHide("upgradeType", 2)]
    public InventoryAnimal costAnimalDecrease;
    [ConditionalEnumHide("upgradeType", 3)]
    public Rarity newRarity;
    [Tooltip("Percentage of original value")]
    [ConditionalEnumHide("upgradeType", 4)]
    public int durationDecrease;
    [ConditionalEnumHide("upgradeType", 5)]
    public Recipe unlockedRecipe;
    [Tooltip("Percentage of original value")]
    [ConditionalEnumHide("upgradeType", 6)]
    public int valueIncrease;
}

[System.Serializable]
public class RecipeProgress
{
    public int animalsProduced;
    [SerializeReference]
    public Recipe recipe;

    [HideInInspector]
    public bool newRecipe = false;

    public float costMultiplier
    {
        get { return recipe.getCostMultiplier(animalsProduced); }
    }
    public Animal animal
    {
        get { return recipe.animal; }
    }
    public int duration
    {
        get { return recipe.getDuration(animalsProduced); }
    }
    public Rarity rarity
    {
        get { return (Rarity)recipe.getRarity(animalsProduced); }
    }

    public List<EssenceAmount> costEssences
    {
        get { return recipe.getEssences(animalsProduced); }
    }
    public List<InventoryArtifact> costArtifacts
    {
        get { return recipe.getArtifacts(animalsProduced); }
    }
    public List<InventoryAnimal> costAnimals
    {
        get { return recipe.getAnimals(animalsProduced); }
    }

    public int level
    {
        get { return recipe.getCurrentLevel(animalsProduced);  }
    }

    public RecipeUpgradeType? nextUpgradeType
    {
        get {
            if (level + 1 < recipe.recipeLevels.Count) {
                return recipe.recipeLevels[level + 1].upgradeType;
            }
            else
            {
                return null;
            }
        }
    }

    public float progress
    {
        get {
            if (level + 1 < recipe.recipeLevels.Count)
            {
                if (level == -1)
                {
                    return (float)animalsProduced / (float)recipe.recipeLevels[level + 1].treshold;
                }
                else
                {
                    return ((float)animalsProduced - (float)recipe.recipeLevels[level].treshold) /
                        ((float)recipe.recipeLevels[level + 1].treshold - (float)recipe.recipeLevels[level].treshold);
                }
            }
            else
            {
                return -1;
            }
        }
    }

    public void animalProduced()
    {
        animalsProduced++;
        if (recipe.getUnlockedRecipe(animalsProduced)!=null)
        {
            RecipeProgress rp = new RecipeProgress();
            rp.animalsProduced = 0;
            rp.recipe = recipe.getUnlockedRecipe(animalsProduced);
            var unlockedUpgrade = recipe.getUnlockedUpgrade(animalsProduced);
            Analytics.LogEvent("recipe_upgrade", new Parameter("animal", recipe.animal.name), new Parameter("upgrade_type", unlockedUpgrade.upgradeType.ToString()), new Parameter("upgrade_threshold", unlockedUpgrade.treshold));
            GameLogic.UnlockRecipe(rp);
        }
        else if (recipe.getUnlockedUpgrade(animalsProduced)!=null)
        {
            NewUpgradeDisplay newRecipeDisplay = Resources.FindObjectsOfTypeAll<NewUpgradeDisplay>()[0];
            var unlockedUpgrade = recipe.getUnlockedUpgrade(animalsProduced);
            Analytics.LogEvent("recipe_upgrade", new Parameter("animal", recipe.animal.name), new Parameter("upgrade_type", unlockedUpgrade.upgradeType.ToString()), new Parameter("upgrade_threshold", unlockedUpgrade.treshold));
            newRecipeDisplay.Open(this, unlockedUpgrade);
        }
    }
}
