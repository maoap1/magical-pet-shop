using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Graphics", menuName = "PetShop/Game Graphics")]
public class GameGraphics : ScriptableObject
{
    private static GameGraphics _THIS;
    public static GameGraphics THIS
    {
        get
        {
            if (_THIS != null) return _THIS;
            _THIS = GameObject.FindObjectOfType<GameGraphics>();
            if (_THIS == null)
            {
                _THIS = Resources.Load<GameGraphics>("Game Graphics");
            }
            GameObject.DontDestroyOnLoad(_THIS);
            return _THIS;
        }
    }

    public Sprite money;
    public Sprite time;
    public Sprite changeRarity;

    public Sprite unknown;
    public Sprite unknownHighlight;

    [ColorUsageAttribute(true, true)]
    public Color commonColor;
    [ColorUsageAttribute(true, true)]
    public Color goodColor;
    [ColorUsageAttribute(true, true)]
    public Color rareColor;
    [ColorUsageAttribute(true, true)]
    public Color epicColor;
    [ColorUsageAttribute(true, true)]
    public Color legendaryColor;

    public Color getRarityColor(Rarity r)
    {
        switch (r)
        {
            case Rarity.Common:
                return commonColor;
            case Rarity.Good:
                return goodColor;
            case Rarity.Rare:
                return rareColor;
            case Rarity.Epic:
                return epicColor;
            case Rarity.Legendary:
                return legendaryColor;
            default:
                return new Color(0,0,0);
        }
    }

    public Sprite getUpgradeSprite(RecipeUpgradeType upgrade)
    {
        switch (upgrade) {
            case RecipeUpgradeType.changeRarity:
                return changeRarity;
            case RecipeUpgradeType.decreaseDuration:
                return time;
            case RecipeUpgradeType.increaseValue:
                return money;
            default:
                return null;
        }
    }
}

public enum RecipeUpgradeType
{
    decreaseEssences = 0,
    decreaseArtifacts = 1,
    decreaseAnimals = 2,
    changeRarity = 3,
    decreaseDuration = 4,
    unlockRecipe = 5,
    increaseValue = 6,
    decreaseRequiredRarity = 7
}
