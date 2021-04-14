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
    public List<PackLeader> packLeaders;
    public int startingMoney;
    public int startingDiamonds;
    public int startingExpeditionSlots;
    public List<EssenceAmount> startingResources;
    public List<ModelAndLevel> startingProducerLevels;
    public List<InventoryAnimal> startingAnimals;
    public List<InventoryArtifact> startingArtifacts;
    public List<RecipeProgress> startingRecipes;

    public float orderFromRecipesProbability = 0.1f;
    public int customerArrivalFrequency = 60;

    public float[] rarityMultipliers = new float[5];
    public float[] rarityPowerMultipliers = new float[5];
    public float[] rarityDeathProbs = new float[5];
    public float[] casualtiesThreshold = new float[4];

    void OnValidate()
    {
        if (rarityMultipliers.Length != 5)
        {
            Debug.LogWarning("Don't change the 'rarityMultipliers' field's array size!");
            Array.Resize(ref rarityMultipliers, 5);
        }
        if (rarityPowerMultipliers.Length != 5) {
            Debug.LogWarning("Don't change the 'rarityPowerMultipliers' field's array size!");
            Array.Resize(ref rarityPowerMultipliers, 5);
        }
        if (rarityDeathProbs.Length != 5) {
            Debug.LogWarning("Don't change the 'rarityDeathProbs' field's array size!");
            Array.Resize(ref rarityDeathProbs, 5);
        }
        if (casualtiesThreshold.Length != 4) {
            Debug.LogWarning("Don't change the 'casualtiesThreshold' field's array size!");
            Array.Resize(ref casualtiesThreshold, 4);
        }
    }

    public float getRarityMultiplier(Rarity rarity)
    {
        return rarityMultipliers[(int)rarity];
    }

    public float GetRarityPowerMultiplier(Rarity rarity) {
        return rarityPowerMultipliers[(int)rarity];
    }

    public float GetRarityDeathProbability(Rarity rarity) {
        return rarityDeathProbs[(int)rarity];
    }

    public void Update()
    {
        long updateTime = Utils.EpochTime();
        float deltaTime = updateTime - PlayerState.THIS.playerTime;
        PlayerState.THIS.playerTime = updateTime;
        for (int i = 0; i < PlayerState.THIS.producers.Count; i++)
        {
            PlayerState.THIS.producers[i].fillRate += PlayerState.THIS.producers[i].productionRate * (deltaTime / 60000);
            if (PlayerState.THIS.producers[i].fillRate >= 1)
            {
                int increaseAmount = (int)Mathf.Floor(PlayerState.THIS.producers[i].fillRate);
                PlayerState.THIS.producers[i].essenceAmount.SetLimit(PlayerState.THIS.producers[i].storageLimit);
                PlayerState.THIS.producers[i].essenceAmount.IncreaseAmount(increaseAmount);
                PlayerState.THIS.producers[i].fillRate -= increaseAmount;
            }
        }
        foreach (CraftedAnimal craftedAnimal in PlayerState.THIS.crafting)
        {
            craftedAnimal.fillRate += (deltaTime / 1000) / craftedAnimal.duration;
        }
        Shop.UpdateCustomers();
        foreach (Expedition expedition in PlayerState.THIS.expeditions) {
            if (expedition.fillRate < 1)
                expedition.fillRate += (deltaTime / 1000) / expedition.expeditionType.duration;
        }
    }
}

[Serializable]
public struct ModelAndLevel
{
    public EssenceProducerModel model;
    public int level;
}
