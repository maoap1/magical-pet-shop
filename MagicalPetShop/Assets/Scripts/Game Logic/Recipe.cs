using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "PetShop/Recipe")]
public class Recipe : ScriptableObject
{
    public Animal animal;
    public List<RecipeLevel> recipeLevels;
    public RecipeLevel GetRecipeLevel(int animalsProduced)
    {
        RecipeLevel result = null;
        foreach (RecipeLevel level in recipeLevels)
        {
            if (animalsProduced > level.treshold)
            {
                result = level;
            }
        }
        return result;
    }
}

[System.Serializable]
public class RecipeLevel
{
    public RecipeUpgradeType upgradeType;
    public int treshold;
    public Cost cost;
    public Rarity rarity;
    public Recipe unlockedRecipe;
    public int duration;
}

public enum RecipeUpgradeType
{
    costUpgrade,
    rarityUpgrade,
    durationUpgrade,
    unlockRecipeUpgrade
}

[CustomPropertyDrawer(typeof(RecipeLevel))]
public class RecipeLevelEditor : PropertyDrawer
{
}

[System.Serializable]
public class RecipeProgress
{
    public int AnimalsProduced;
    [SerializeReference]
    public Recipe recipe;
}
