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
    public int level;
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

    public float speed = 1;

    //[HideInInspector]
    public List<int> unluckySeries;

    [HideInInspector]
    public long lastArrivalTime;
    [HideInInspector]
    public Customer[] customers;

    [HideInInspector]
    public List<ExpeditionDifficulty> lastExpeditionDifficulties;

    private bool initialized = false;
    private bool tutorial = false;
    public int currentTutorial = 0;
    public int tutorialProgress = 0;

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
            THIS.Init();
            return _THIS;
        }
    }
    
    public void Save()
    {
        if (tutorial)
        {
            currentTutorial = Tutorials.THIS.currentIndex;
            tutorialProgress = Tutorials.THIS.tutorials[Tutorials.THIS.currentIndex].progress;
        }
        lastArrivalTime = Shop.lastArrivalTime;
        customers = Shop.customers;
        string json = JsonUtility.ToJson(this);
        json = version.ToString() + "," + json;
        string path = getPath();
        Debug.Log("trying to save to: " + path);
        System.IO.File.WriteAllText(path, json);
    }
    private PlayerState() {
    }

    public void Init()
    {
        if (initialized) return;
        initialized = true;

        if (int.TryParse(Application.version.Split('.')[1], out int ver))
        {
            this.version = ver;
        }
        else
        {
            this.version = GameLogic.THIS.version;
            Debug.LogError("Automatic versioning not working");
        }
        string path = getPath();
        if (File.Exists(path))
        {
            Debug.Log("loading from: " + path);
            var json = File.ReadAllText(path);
            int index = json.IndexOf(',');
            int saveVersion = int.Parse(json.Substring(0, index));
            if (saveVersion == version)
            {
                string stateJson = json.Substring(index + 1);
                JsonUtility.FromJsonOverwrite(stateJson, this);
                Shop.lastArrivalTime = lastArrivalTime;
                Shop.customers = customers;
                Tutorials.THIS.currentIndex = this.currentTutorial;
                if (Tutorials.THIS.currentIndex < Tutorials.THIS.tutorials.Count) {
                    Tutorials.THIS.tutorials[Tutorials.THIS.currentIndex].startWithProgress(this.tutorialProgress);
                }
            }
            else
            {
                LoadFromGameLogic();
            }
        }
        else
        {
            LoadFromGameLogic();
        }
        GameLogic.THIS.InitTutorialVariables();
    }

    public void Start()
    {
        Init();  
    }

    public void LoadFromGameLogic()
    {
        if (tutorial)
        {
            Tutorials.THIS.init();
        }
        this.money = GameLogic.THIS.startingMoney;
        this.diamonds = GameLogic.THIS.startingDiamonds;
        this.level = 1;
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
                if (e.essence.associatedLocation == rp.animal.category)
                {
                    e.unlocked = true;
                }
            }
            if (PlayerState.THIS.level < r.animal.level)
            {
                PlayerState.THIS.level = r.animal.level;
            }
        }
        this.lastExpeditionDifficulties = new List<ExpeditionDifficulty>();
        for (int i = 0; i < GameLogic.THIS.expeditions.Count; ++i) {
            this.lastExpeditionDifficulties.Add(ExpeditionDifficulty.Easy);
        }
        this.expeditions = new List<Expedition>();
        this.crafting = new List<CraftedAnimal>();
        this.unluckySeries = new List<int>();
        this.craftingSlots = 1;
        unluckySeries.AddRange(new int[] {0, 0, 0, 0});
        this.playerTime = Utils.EpochTime();
        Shop.lastArrivalTime = playerTime;
        Shop.customers = new Customer[5];
        customers = Shop.customers;
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
        if (tutorial)
        {
            Tutorials.THIS.Update();
        }
    }

}
