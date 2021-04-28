using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class PlayerState : MonoBehaviour
{
    private int version;
    [HideInInspector]
    public int money;
    [HideInInspector]
    public int diamonds;
    [Tooltip("Don't set in editor")]
    public int level; // TODO: The level should be correctly updated when a recipe of higher level is discovered
    [Tooltip("Don't set in editor")]
    [SerializeReference]
    public List<EssenceAmount> resources;
    [HideInInspector]
    public List<EssenceProducer> producers;
    [HideInInspector]
    public List<InventoryAnimal> animals;
    [HideInInspector]
    public List<InventoryArtifact> artifacts;
    [HideInInspector]
    public int numberOfExpeditionSlots;
    [HideInInspector]
    public List<Expedition> expeditions;
    [Tooltip("Don't set in editor")]
    public List<Pack> packs;
    [Tooltip("Don't set in editor")]
    public List<CraftedAnimal> crafting;
    [HideInInspector]
    public int craftingSlots;
    [HideInInspector]
    public List<RecipeProgress> recipes;
    [HideInInspector]
    public long playerTime;

    //[HideInInspector]
    public List<int> unluckySeries;

    [HideInInspector]
    public long lastArrivalTime;
    [HideInInspector]
    public Customer[] customers;

    private static PlayerState _THIS;
    public static PlayerState THIS
    {
        get
        {
            if (_THIS != null) return _THIS;
            _THIS = GameObject.FindObjectOfType<PlayerState>();
            if (_THIS == null)
            {
                GameObject ps = new GameObject();
                ps.name = "PlayerState";
                _THIS = ps.AddComponent<PlayerState>();                
            }
            GameObject.DontDestroyOnLoad(_THIS);
            return _THIS;
        }
    }
    
    public void Save()
    {
        lastArrivalTime = Shop.lastArrivalTime;
        customers = Shop.customers;
        string json = JsonUtility.ToJson(this);
<<<<<<< HEAD
        json = version.ToString() + "," + json;
        string path = getPath();
=======
<<<<<<< Updated upstream
        string path = Application.persistentDataPath + "/PlayerState.json";
=======
        json = version.ToString() + "," + json;
        string path = getPath();
>>>>>>> Stashed changes
>>>>>>> parent of 24a2026 (Revert "fixed some issues")
        Debug.Log("trying to save to: " + path);
        System.IO.File.WriteAllText(path, json);
    }
    private PlayerState() {
    }

    public void Start()
    {
<<<<<<< HEAD
        this.version = GameLogic.THIS.version;
        string path = getPath();
=======
<<<<<<< Updated upstream
        string path = Application.persistentDataPath + "/PlayerState.json";
=======
        this.version = GameLogic.THIS.version;
        string path = getPath();
>>>>>>> Stashed changes
>>>>>>> parent of 24a2026 (Revert "fixed some issues")
        if (File.Exists(path))
        {
            Debug.Log("loading from: " + path);
            var json = File.ReadAllText(path);
<<<<<<< HEAD
            int index = json.IndexOf(',');
            int saveVersion = int.Parse(json.Substring(0, index));
            if (saveVersion == version)
            {
                string stateJson = json.Substring(index + 1);
                JsonUtility.FromJsonOverwrite(stateJson, this);
                Shop.lastArrivalTime = lastArrivalTime;
                Shop.customers = customers;
            }
            else
            {
                loadFromGameLogic();
            }
=======
<<<<<<< Updated upstream
            JsonUtility.FromJsonOverwrite(json, this);
            Shop.lastArrivalTime = lastArrivalTime;
            Shop.customers = customers;
        }
        else
        {
            this.money = GameLogic.THIS.startingMoney;
            this.diamonds = GameLogic.THIS.startingDiamonds;
            this.level = 1;
            this.producers = new List<EssenceProducer>();
            this.resources = new List<EssenceAmount>();
            foreach (var p in GameLogic.THIS.startingProducerLevels)
=======
            int index = json.IndexOf(',');
            int saveVersion = int.Parse(json.Substring(0, index));
            if (saveVersion == version)
>>>>>>> Stashed changes
            {
                string stateJson = json.Substring(index + 1);
                JsonUtility.FromJsonOverwrite(stateJson, this);
                Shop.lastArrivalTime = lastArrivalTime;
                Shop.customers = customers;
            }
            else
            {
                loadFromGameLogic();
            }
>>>>>>> parent of 24a2026 (Revert "fixed some issues")
        }
        else
        {
            loadFromGameLogic();
        }
    }

    private void loadFromGameLogic()
    {
        this.money = GameLogic.THIS.startingMoney;
        this.diamonds = GameLogic.THIS.startingDiamonds;
        this.level = 1;
        this.numberOfExpeditionSlots = GameLogic.THIS.startingExpeditionSlots;
        this.producers = new List<EssenceProducer>();
        this.resources = new List<EssenceAmount>();
        foreach (var p in GameLogic.THIS.startingProducerLevels)
        {
            this.producers.Add(new EssenceProducer(p.model, p.level));
        }
        foreach (var rc in GameLogic.THIS.startingResources)
        {
            EssenceProducer p = producers.Find(x => x.essenceAmount.essence.essenceName == rc.essence.name);
            p.essenceAmount.amount = rc.amount;
            resources.Add(p.essenceAmount);
        }
        this.animals = new List<InventoryAnimal>();
        foreach (var a in GameLogic.THIS.startingAnimals)
        {
            InventoryAnimal ia = new InventoryAnimal();
            ia.animal = a.animal;
            ia.count = a.count;
            ia.rarity = a.rarity;
            animals.Add(ia);
        }
        this.artifacts = new List<InventoryArtifact>();
        foreach (var a in GameLogic.THIS.startingArtifacts)
        {
            InventoryArtifact ia = new InventoryArtifact();
            ia.artifact = a.artifact;
            ia.count = a.count;
            artifacts.Add(ia);
        }
        this.packs = new List<Pack>();
        foreach (var leader in GameLogic.THIS.packLeaders)
        {
            Pack pack = new Pack(leader);
            this.packs.Add(pack);
        }
        this.recipes = new List<RecipeProgress>();
        foreach (var rp in GameLogic.THIS.startingRecipes)
        {
            RecipeProgress r = new RecipeProgress();
            r.recipe = rp.recipe;
            r.animalsProduced = rp.animalsProduced;
            recipes.Add(r);
            foreach (var e in resources)
            {
<<<<<<< HEAD
                if (e.essence.associatedLocation == rp.animal.category)
=======
<<<<<<< Updated upstream
                RecipeProgress r = new RecipeProgress();
                r.recipe = rp.recipe;
                r.animalsProduced = rp.animalsProduced;
                recipes.Add(r);
                foreach (var e in resources)
>>>>>>> parent of 24a2026 (Revert "fixed some issues")
                {
                    e.unlocked = true;
                }
            }
<<<<<<< HEAD
=======
            this.expeditions = new List<Expedition>();
            this.crafting = new List<CraftedAnimal>();
            this.playerTime = Utils.EpochTime();
            Shop.lastArrivalTime = playerTime;
            Save();
=======
                if (e.essence.associatedLocation == rp.animal.category)
                {
                    e.unlocked = true;
                }
            }
>>>>>>> parent of 24a2026 (Revert "fixed some issues")
            if (PlayerState.THIS.level < r.animal.level)
            {
                PlayerState.THIS.level = r.animal.level;
            }
<<<<<<< HEAD
=======
>>>>>>> Stashed changes
>>>>>>> parent of 24a2026 (Revert "fixed some issues")
        }
        this.expeditions = new List<Expedition>();
        this.crafting = new List<CraftedAnimal>();
        this.unluckySeries = new List<int>();
        this.craftingSlots = 1;
        unluckySeries.AddRange(new int[] {0, 0, 0, 0});
        this.playerTime = Utils.EpochTime();
        Shop.lastArrivalTime = playerTime;
        PacksManager.UnlockPacks();
        Save();
    }

    private string getPath()
    {
        string path = Application.persistentDataPath + "/PlayerStateBuild.json";
#if UNITY_EDITOR
        path = Application.persistentDataPath + "/PlayerState.json";
#endif
        return path;
    }

    public void Update()
    {
        GameLogic.THIS.Update();
    }

}
