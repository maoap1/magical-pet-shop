using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Inventory
{
    // TODO Implement Methods

    public static List<InventoryAnimal> GetOrderedAnimals() {
        // TODO: Remove the following line when PLayerState is initialized correctly
        if (PlayerState.THIS.animals == null) return new List<InventoryAnimal>();
        // descending order according to value - sorted in place, may be redone later, if necessary
        PlayerState.THIS.animals.Sort((a1, a2) => a2.animal.value.CompareTo(a1.animal.value));
        return PlayerState.THIS.animals;
    }

    public static List<InventoryArtifact> GetOrderedArtifacts() {
        // TODO: Remove the following line when PLayerState is initialized correctly
        if (PlayerState.THIS.artifacts == null)
            return new List<InventoryArtifact>();
        // TODO: Artifacts don't have value - what is the correct order?
        return PlayerState.THIS.artifacts;
    }

    public static void AddToInventory(Cost cost) {
        foreach (InventoryAnimal animal in cost.animals) {
            AddToInventory(animal);
        }
        foreach (InventoryArtifact artifact in cost.artifacts) {
            AddToInventory(artifact);
        }
        AddToInventory(cost.resources);
        AddToInventory(cost.money);
    }

    public static bool TakeFromInventory(Cost cost) {
        // first check - so that we don't pay part of cost before failure
        if (!HasInInventory(cost))
            return false;
        foreach (InventoryAnimal animal in cost.animals) {
            TakeFromInventory(animal);
        }
        foreach (InventoryArtifact artifact in cost.artifacts) {
            TakeFromInventory(artifact);
        }
        TakeFromInventory(cost.resources);
        TakeFromInventory(cost.money);
        return true;
    }

    public static bool HasInInventory(Cost cost) {
        foreach (InventoryAnimal animal in cost.animals) {
            if (!HasInInventory(animal)) return false;
        }
        foreach (InventoryArtifact artifact in cost.artifacts) {
            if (!HasInInventory(artifact)) return false;
        }
        if (!HasInInventory(cost.resources)) return false;
        if (!HasInInventory(cost.money)) return false;
        return true;
    }



    private static void AddToInventory(InventoryAnimal animal) {
        var result = PlayerState.THIS.animals.Find(otherAnimal => animal == otherAnimal); // equality based on name, value and rarity
        if (result != null) result.count += animal.count;
        else PlayerState.THIS.animals.Add(animal);
    }

    private static void AddToInventory(InventoryArtifact artifact) {
        var result = PlayerState.THIS.artifacts.Find(otherArtifact => artifact == otherArtifact); // equality based on name
        if (result != null) result.count += artifact.count;
        else PlayerState.THIS.artifacts.Add(artifact);
    }

    private static void AddToInventory(List<EssenceAmount> essenceAmounts) {
        foreach (EssenceAmount essenceAmount in essenceAmounts) {
            AddToInventory(essenceAmount);
        }
    }

    private static void AddToInventory(EssenceAmount essenceAmount) {
        var result = PlayerState.THIS.resources.Find(otherEssence => essenceAmount.essence == otherEssence.essence);
        if (result != null) result.amount += essenceAmount.amount;
        else PlayerState.THIS.resources.Add(essenceAmount);
    }

    private static void AddToInventory(int money) {
        PlayerState.THIS.money += money;
    }



    private static void TakeFromInventory(InventoryAnimal animal) {
        // without checking that there are enough items in the inventory
        var result = PlayerState.THIS.animals.Find(otherAnimal => animal == otherAnimal); // equality based on name, value and rarity
        if (result != null) {
            result.count -= animal.count;
            if (result.count <= 0) PlayerState.THIS.animals.Remove(result);
        }
    }

    private static void TakeFromInventory(InventoryArtifact artifact) {
        // without checking that there are enough items in the inventory
        var result = PlayerState.THIS.artifacts.Find(otherArtifact => artifact == otherArtifact); // equality based on name
        if (result != null) {
            result.count -= artifact.count;
            if (result.count <= 0) PlayerState.THIS.artifacts.Remove(result);
        }
    }

    private static void TakeFromInventory(List<EssenceAmount> essenceAmounts) {
        // without checking that there are enough items in the inventory
        foreach (EssenceAmount essenceAmount in essenceAmounts) {
            TakeFromInventory(essenceAmount);
        }
    }

    private static void TakeFromInventory(EssenceAmount essenceAmount) {
        // without checking that there are enough items in the inventory
        var result = PlayerState.THIS.resources.Find(otherEssence => essenceAmount.essence == otherEssence.essence);
        if (result != null) {
            result.amount -= essenceAmount.amount;
        }
    }

    private static void TakeFromInventory(int money) {
        // without checking that there are enough items in the inventory
        PlayerState.THIS.money -= money;
    }



    private static bool HasInInventory(InventoryAnimal animal) {
        var result = PlayerState.THIS.animals.Find(otherAnimal => animal == otherAnimal); // equality based on name, value and rarity
        return result != null && result.count >= animal.count;
    }

    private static bool HasInInventory(InventoryArtifact artifact) {
        var result = PlayerState.THIS.artifacts.Find(otherArtifact => artifact == otherArtifact); // equality based on name
        return result != null && result.count >= artifact.count;
    }

    private static bool HasInInventory(List<EssenceAmount> essenceAmounts) {
        foreach (EssenceAmount essenceAmount in essenceAmounts) {
            if (!HasInInventory(essenceAmount)) return false;
        }
        return true;
    }

    private static bool HasInInventory(EssenceAmount essenceAmount) {
        var result = PlayerState.THIS.resources.Find(otherEssence => essenceAmount.essence == otherEssence.essence);
        return result != null && result.amount >= essenceAmount.amount;
    }

    private static bool HasInInventory(int money) {
        return PlayerState.THIS.money >= money;
    }


    #region Other methods which might be useful

    public static void AddToInventory(Animal animal, int count = 1) {
        var result = PlayerState.THIS.animals.Find(otherAnimal => animal == otherAnimal.animal); // equality based on name, value and rarity
        if (result != null) result.count += count;
        else PlayerState.THIS.animals.Add(new InventoryAnimal { animal = animal, count = count });
    }

    public static void AddToInventory(Artifact artifact, int count = 1) {
        var result = PlayerState.THIS.artifacts.Find(otherArtifact => artifact == otherArtifact.artifact); // equality based on name
        if (result != null) result.count += count;
        else PlayerState.THIS.artifacts.Add(new InventoryArtifact { artifact = artifact, count = count });
    }


    public static void TakeFromInventory(Animal animal, int count = 1) {
        // without checking that there are enough items in the inventory
        var result = PlayerState.THIS.animals.Find(otherAnimal => animal == otherAnimal.animal); // equality based on name, value and rarity
        if (result != null) {
            result.count -= count;
            if (result.count <= 0) PlayerState.THIS.animals.Remove(result);
        }
    }

    public static void TakeFromInventory(Artifact artifact, int count = 1) {
        // without checking that there are enough items in the inventory
        var result = PlayerState.THIS.artifacts.Find(otherArtifact => artifact == otherArtifact.artifact); // equality based on name
        if (result != null) {
            result.count -= count;
            if (result.count <= 0) PlayerState.THIS.artifacts.Remove(result);
        }
    }

    public static bool HasInInventory(Animal animal, int count = 1) {
        var result = PlayerState.THIS.animals.Find(otherAnimal => animal == otherAnimal.animal); // equality based on name, value and rarity
        return result != null && result.count >= count;
    }

    public static bool HasInInventory(Artifact artifact, int count = 1) {
        var result = PlayerState.THIS.artifacts.Find(otherArtifact => artifact == otherArtifact.artifact); // equality based on name
        return result != null && result.count >= count;
    }

    #endregion
}

[Serializable]
public class InventoryAnimal : IEquatable<InventoryAnimal> {
    public Animal animal;
    public Rarity rarity;
    public int count;

    public bool Equals(InventoryAnimal other) {
        if (other == null) return false;
        return this.animal == other.animal;
    }

    public override bool Equals(object obj) {
        if (obj == null) return false;

        InventoryAnimal other = obj as InventoryAnimal;
        if (other == null) return false;
        else return Equals(other);
    }

    public override int GetHashCode() {
        return this.animal.GetHashCode();
    }

    public static bool operator ==(InventoryAnimal animal1, InventoryAnimal animal2) {
        if (((object)animal1) == null || ((object)animal2) == null)
            return object.Equals(animal1, animal2);
        return animal1.Equals(animal2);
    }

    public static bool operator !=(InventoryAnimal animal1, InventoryAnimal animal2) {
        return !(animal1 == animal2);
    }
}

[Serializable]
public class InventoryArtifact : IEquatable<InventoryArtifact> {
    public Artifact artifact;
    public int count;

    public bool Equals(InventoryArtifact other) {
        if (other == null) return false;
        return this.artifact == other.artifact;
    }

    public override bool Equals(object obj) {
        if (obj == null) return false;

        InventoryArtifact other = obj as InventoryArtifact;
        if (other == null) return false;
        else return Equals(other);
    }

    public override int GetHashCode() {
        return this.artifact.GetHashCode();
    }

    public static bool operator ==(InventoryArtifact artifact1, InventoryArtifact artifact2) {
        if (((object)artifact1) == null || ((object)artifact2) == null)
            return object.Equals(artifact1, artifact2);
        return artifact1.Equals(artifact2);
    }

    public static bool operator !=(InventoryArtifact artifact1, InventoryArtifact artifact2) {
        return !(artifact1 == artifact2);
    }
}

[Serializable]
public struct Cost
{
    public List<InventoryAnimal> animals;
    public List<InventoryArtifact> artifacts;
    public List<EssenceAmount> resources;
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
