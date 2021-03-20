using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class PlayerState : MonoBehaviour
{
    [Tooltip("Don't set in editor")]
    public int money;
    [Tooltip("Don't set in editor")]
    [SerializeReference]
    public List<EssenceAmount> resources;
    [Tooltip("Don't set in editor")]
    public List<EssenceProducer> producers;
    [Tooltip("Don't set in editor")]
    public List<InventoryAnimal> animals;
    [Tooltip("Don't set in editor")]
    public List<InventoryArtifact> artifacts;
    [Tooltip("Don't set in editor")]
    public List<Expedition> expeditions;
    [Tooltip("Don't set in editor")]
    public List<CraftedAnimal> crafting;
    public long playerTime;

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
        string json = JsonUtility.ToJson(this);
        string path = Application.persistentDataPath + "/PlayerState.json";
        Debug.Log("trying to save to: " + path);
        System.IO.File.WriteAllText(path, json);
    }
    private PlayerState() {
    }

    public void Start()
    {
        string path = Application.persistentDataPath + "/PlayerState.json";
        if (File.Exists(path))
        {
            Debug.Log("loading from: " + path);
            var json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);
        }
        else
        {
            this.money = GameLogic.THIS.startingMoney;
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
            this.animals = GameLogic.THIS.startingAnimals;
            this.artifacts = GameLogic.THIS.startingArtifacts;
            this.expeditions = new List<Expedition>();
            this.crafting = new List<CraftedAnimal>();
            this.playerTime = Utils.EpochTime();
            Save();
        }
    }

    public void Update()
    {
        long updateTime = Utils.EpochTime();
        float deltaTime = updateTime - playerTime;
        playerTime = updateTime;
        for (int i = 0; i<producers.Count; i++)
        {
            producers[i].fillRate += producers[i].productionRate * (deltaTime / 60000);
            if (producers[i].fillRate>=1)
            {
                int increaseAmount = (int)Mathf.Floor(producers[i].fillRate);
                producers[i].essenceAmount.IncreaseAmount(increaseAmount);
                producers[i].fillRate -= increaseAmount;
            }
        }
        foreach (CraftedAnimal craftedAnimal in crafting)
        {
            craftedAnimal.fillRate += (deltaTime / 1000) / craftedAnimal.recipe.recipe.GetRecipeLevel(0).duration;
        }
    }

}
