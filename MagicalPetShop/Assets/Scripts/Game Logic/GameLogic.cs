using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase.Analytics;

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


    public int version;
    public MergingSettings mergingSettings;
    [Header("Expeditions")]
    public List<ExpeditionType> expeditions;
    public List<PackLeader> packLeaders;
    public float[] casualtiesThreshold = new float[4];
    [Header("Starting Settings")]
    public int startingMoney;
    public int startingDiamonds;
    public List<EssenceAmount> startingResources;
    public List<ModelAndLevel> startingProducerLevels;
    public List<InventoryAnimal> startingAnimals;
    public List<InventoryArtifact> startingArtifacts;
    public List<RecipeProgress> startingRecipes;

    [Header("Customer Settings")]
    public float orderFromRecipesProbability = 0.1f;
    public int customerArrivalFrequency = 60;

    [Header("Rarity Multipliers")]
    public float[] rarityMultipliers = new float[5];
    public float[] rarityPowerMultipliers = new float[5];
    public float[] rarityDeathProbs = new float[5];

    [Header("Crafting Luck")]
    [Tooltip("Float values between 0 and 1")]
    public float[] upgradeProbabilities = new float[4];
    public int[] automaticUpgradeTresholds = new int[4];

    [Header("Crafting slots")]
    public CraftingSlotUpgrade[] craftingSlotUpgrades = new CraftingSlotUpgrade[4];

    [Header("Money for levels")]
    public int[] moneyForLevels = new int[5];


    [Header("Helper variables for tutorial")]
    public bool inSellingOverlay = false;
    public bool inInventory = false;
    public bool inCrafting = false;
    public bool inMerging = false;
    public bool buyingCraftingSlot = false;
    public bool inRecipeInfo = false;
    public bool inNewRecipeDisplay = false;
    public bool inNewLevelDisplay = false;
    public bool inPackLeaderSelection = false;
    public bool inExpeditionList = false;
    public bool inExpedition = false;
    public bool inPackOverview = false;
    public bool inAnimalsUI = false;
    public LocationType currentRecipeCategory = null;
    public EssenceProducer essenceProducerOpened = null;

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
        if (upgradeProbabilities.Length != 4) {
            Debug.LogWarning("Don't change the 'upgradeProbabilities' field's array size!");
            Array.Resize(ref upgradeProbabilities, 4);
        }
        if (automaticUpgradeTresholds.Length != 4) {
            Debug.LogWarning("Don't change the 'automaticUpgradeTresholds' field's array size!");
            Array.Resize(ref automaticUpgradeTresholds, 4);
        }
        if (craftingSlotUpgrades.Length != 4)
        {
            Debug.LogWarning("Don't change the 'craftingSlotUpgrades' field's array size!");
            Array.Resize(ref craftingSlotUpgrades, 4);
        }

        if (moneyForLevels.Length != 5)
        {
            Debug.LogWarning("Don't change the 'moneyForLevels' field's array size!");
            Array.Resize(ref moneyForLevels, 5);
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

    public static void UnlockEssence(LocationType category)
    {
        foreach (var e in PlayerState.THIS.resources)
        {
            if (e.essence.associatedLocation == category && !e.unlocked)
            {
                e.unlocked = true;
                e.amount = PlayerState.THIS.producers.Find(producer => producer.essenceAmount.essence == e.essence).storageLimit;
                Analytics.LogEvent("essence_unlocked", new Parameter("essence", e.essence.essenceName));
            }
        }
    }

    public static void UnlockRecipe(RecipeProgress rp)
    {
        if (PlayerState.THIS.recipes.Find(r => r.recipe.animal == rp.animal) == null)
        {
            rp.newRecipe = true;
            PlayerState.THIS.recipes.Add(rp);
        }
        Analytics.LogEvent("recipe_unlocked", new Parameter("animal", rp.animal.name), new Parameter("animal_level", rp.animal.level));
        if (PlayerState.THIS.level<rp.animal.level)
        {
            PlayerState.THIS.level = rp.animal.level;
            PacksManager.UnlockPacks();
            NewRecipeDisplay newRecipeDisplay = Resources.FindObjectsOfTypeAll<NewRecipeDisplay>()[0];
            newRecipeDisplay.Open(rp, true);
        }
        else
        {
            NewRecipeDisplay newRecipeDisplay = Resources.FindObjectsOfTypeAll<NewRecipeDisplay>()[0];
            newRecipeDisplay.Open(rp, false);
        }
        GameLogic.UnlockEssence(rp.animal.category);
    }

    public void SetSpeed(int speed)
    {
        PlayerState.THIS.speed = speed;
    }

    public void Update()
    {
        long updateTime = Utils.EpochTime();
        float deltaTime = PlayerState.THIS.speed*(updateTime - PlayerState.THIS.playerTime);
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
        foreach (Expedition expedition in PlayerState.THIS.expeditions)
        {
            expedition.fillRate += (deltaTime / 1000) / expedition.expeditionType.duration;
        }
        Shop.UpdateCustomers();
    }

    public void InitTutorialVariables()
    {
        inSellingOverlay = false;
        inInventory = false;
        inCrafting = false;
        inMerging = false;
        buyingCraftingSlot = false;
        inRecipeInfo = false;
        inNewRecipeDisplay = false;
        inNewLevelDisplay = false;
        inPackLeaderSelection = false;
        inExpeditionList = false;
        inExpedition = false;
        inPackOverview = false;
        inAnimalsUI = false;
        currentRecipeCategory = null;
        essenceProducerOpened = null;
    }
}

[Serializable]
public struct ModelAndLevel
{
    public EssenceProducerModel model;
    public int level;
}

[Serializable]
public struct CraftingSlotUpgrade
{
    public int level;
    public int cost;
}
