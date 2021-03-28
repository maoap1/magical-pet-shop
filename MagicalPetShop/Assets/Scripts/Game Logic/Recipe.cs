using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
        List<EssenceAmount> essences = baseCostEssences;
        for (int i = 0; i < recipeLevels.Count; i++)
        {
            if (animalsProduced >= recipeLevels[i].treshold && recipeLevels[i].upgradeType == RecipeUpgradeType.decreaseEssences)
            {
                foreach (EssenceAmount amount in recipeLevels[i].costEssencesDecrease)
                {
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
        }
        return essences;
    }


    public List<InventoryArtifact> getArtifacts(int animalsProduced)
    {
        List<InventoryArtifact> artifacts = baseCostArtifacts;
        for (int i = 0; i < recipeLevels.Count; i++)
        {
            if (animalsProduced >= recipeLevels[i].treshold && recipeLevels[i].upgradeType == RecipeUpgradeType.decreaseArtifacts)
            {
                foreach (InventoryArtifact amount in recipeLevels[i].costArtifactsDecrease)
                {
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
        }
        return artifacts;
    }


    public List<InventoryAnimal> getAnimals(int animalsProduced)
    {
        List<InventoryAnimal> animals = baseCostAnimals;
        for (int i = 0; i < recipeLevels.Count; i++)
        {
            if (animalsProduced >= recipeLevels[i].treshold && recipeLevels[i].upgradeType == RecipeUpgradeType.decreaseAnimals)
            {
                foreach (InventoryAnimal amount in recipeLevels[i].costAnimalsDecrease)
                {
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
        }
        return animals;
    }


    public int getDuration(int animalsProduced)
    {
        int duration = baseDuration;
        for (int i = 0; i < recipeLevels.Count; i++)
        {
            if (animalsProduced >= recipeLevels[i].treshold && recipeLevels[i].upgradeType==RecipeUpgradeType.decreaseDuration)
            {
                duration -= recipeLevels[i].durationDecrease;
            }
        }
        return duration;
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
        if (recipeLevels[level].treshold == animalsProduced)
        {
            return recipeLevels[level].unlockedRecipe;
        }
        return null;
    }
}

[System.Serializable]
public class RecipeLevel
{
    public int treshold;
    public RecipeUpgradeType upgradeType;
    //Hide everythong except 0
    //Property drawer doesn't work on lists
    //[ConditionalEnumHide("upgradeType", 0)]
    public List<EssenceAmount> costEssencesDecrease;
    //[ConditionalEnumHide("upgradeType", 1)]
    public List<InventoryArtifact> costArtifactsDecrease;
    //[ConditionalEnumHide("upgradeType", 2)]
    public List<InventoryAnimal> costAnimalsDecrease;
    [ConditionalEnumHide("upgradeType", 3)]
    public Rarity newRarity;
    [ConditionalEnumHide("upgradeType", 4)]
    public int durationDecrease;
    [ConditionalEnumHide("upgradeType", 5)]
    public Recipe unlockedRecipe;
}

public enum RecipeUpgradeType
{
    decreaseEssences = 0,
    decreaseArtifacts = 1,
    decreaseAnimals = 2,
    changeRarity = 3,
    decreaseDuration = 4,
    unlockRecipe = 5
}

[System.Serializable]
public class RecipeProgress
{
    public int animalsProduced;
    [SerializeReference]
    public Recipe recipe;
    public float duration
    {
        get { return (float)recipe.getDuration(animalsProduced); }
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

    public float progress
    {
        get {
            if (recipe.getCurrentLevel(animalsProduced) + 1 < recipe.recipeLevels.Count)
            {
                return ((float)recipe.recipeLevels[recipe.getCurrentLevel(animalsProduced)].treshold - (float)animalsProduced) /
                    ((float)recipe.recipeLevels[recipe.getCurrentLevel(animalsProduced)+1].treshold - (float)recipe.recipeLevels[recipe.getCurrentLevel(animalsProduced)].treshold);
            }
            else
            {
                return -1;
            }
        }
    }
}
