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
                _THIS = Resources.FindObjectsOfTypeAll<GameLogic>()[0];
            }
            GameObject.DontDestroyOnLoad(_THIS);
            return _THIS;
        }
    }

    public List<Recipe> recipes;
    public List<ExpeditionType> expeditions;
    public List<ProductionLevel> productionLevels;
    public int startingMoney;
    public Essences startingResources;
    public EssenceProducers startingProducers;
    public List<InventoryAnimal> startingAnimals;
    public List<InventoryArtifact> startingArtifacts;
}

[Serializable]
public class ProductionLevel
{
    public int level;
    public int productionRate;
    public string name;
    public Cost cost;
}
