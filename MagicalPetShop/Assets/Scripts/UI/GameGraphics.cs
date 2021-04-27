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
    public Sprite decreaseEssences;
    public Sprite decreaseArtifacts;
    public Sprite decreaseAnimals;
    public Sprite changeRarity;
    public Sprite decreaseDuration;
    public Sprite unlockRecipe;

    public Sprite unknown;

    public Sprite getUpgradeSprite(RecipeUpgradeType upgrade)
    {
        switch (upgrade) {
            case RecipeUpgradeType.decreaseEssences:
                return decreaseEssences;
            case RecipeUpgradeType.decreaseArtifacts:
                return decreaseArtifacts;
            case RecipeUpgradeType.decreaseAnimals:
                return decreaseAnimals;
            case RecipeUpgradeType.changeRarity:
                return changeRarity;
            case RecipeUpgradeType.decreaseDuration:
                return decreaseDuration;
            case RecipeUpgradeType.unlockRecipe:
                return unlockRecipe;
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
    unlockRecipe = 5
}
