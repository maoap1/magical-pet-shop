using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Game Logic", menuName = "PetShop/Game Logic")]
public class GameLogic : ScriptableObject
{
    private static GameLogic _THIS;
    public static GameLogic THIS
    {
        get
        {
            if (_THIS != null) return _THIS;
            _THIS = GameObject.FindObjectOfType<GameLogic>();
            if (_THIS == null)
            {
                _THIS = Resources.Load<GameLogic>("Game Logic");
            }
            GameObject.DontDestroyOnLoad(_THIS);
            return _THIS;
        }
    }

    public List<ExpeditionType> expeditions;
    public int startingMoney;
    public int startingDiamonds;
    public List<EssenceAmount> startingResources;
    public List<ModelAndLevel> startingProducerLevels;
    public List<InventoryAnimal> startingAnimals;
    public List<InventoryArtifact> startingArtifacts;
    public List<RecipeProgress> startingRecipes;
}

[Serializable]
public struct ModelAndLevel
{
    public EssenceProducerModel model;
    public int level;
}
