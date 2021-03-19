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
public class InventoryAnimal {
    public Animal animal;
    public int count;
}

[Serializable]
public class InventoryArtifact
{
    public Artifact artifact;
    public int count;
}

[Serializable]
public struct Cost
{
    public List<InventoryAnimal> animals;
    public List<InventoryArtifact> artifacts;
    public List<EssenceCount> resources;
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
