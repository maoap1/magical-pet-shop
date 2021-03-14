using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Inventory
{
    // TODO Implement Methods
    //public static bool TakeFromInventory(Cost cost){}
    //public static bool HasInInventory(Cost cost){}
    //public static List<InventoryAnimal> GetOrderedAnimals(){}
    //public static List<InventoryArtifact> GetOrderedArtifacts() {}
}

[Serializable]
public struct InventoryAnimal {
    public Animal animal;
    public int count;
}

[Serializable]
public struct InventoryArtifact
{
    public Artifact artifact;
    public int count;
}

[Serializable]
public struct Cost
{
    public List<InventoryAnimal> animals;
    public List<InventoryArtifact> artifacts;
    public Essences resources;
    public int money;
}
public enum Rarity
{
    Common,
    Great,
    Flawless,
    Epic,
    Legendary
}
