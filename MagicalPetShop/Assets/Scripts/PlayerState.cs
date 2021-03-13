using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class PlayerState : MonoBehaviour
{
    public int money;
    public Essences resources;
    public EssenceProducers producers;
    public List<InventoryAnimal> animals;
    public List<InventoryArtifact> artifacts;
    public List<Expedition> expeditions;
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
        System.IO.File.WriteAllText(path, json);
    }
    private PlayerState() {
    }

    public void Start()
    {
        string path = Application.persistentDataPath + "/PlayerState.json";
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);
        }
        else
        {
            this.money = GameLogic.THIS.startingMoney;
            this.resources = GameLogic.THIS.startingResources;
            this.producers = GameLogic.THIS.startingProducers;
            this.animals = GameLogic.THIS.startingAnimals;
            this.artifacts = GameLogic.THIS.startingArtifacts;
            this.expeditions = new List<Expedition>();
            this.crafting = new List<CraftedAnimal>();
            this.playerTime = Utils.EpochTime();
            Save();
        }
    }

}

[Serializable]
public struct Essences
{
    public List<Essence> essences;
}
