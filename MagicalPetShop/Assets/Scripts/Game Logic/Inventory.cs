using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Inventory
{

    public static int GetNumberOfAnimals() {
        int count = 0;
        foreach (var animal in PlayerState.THIS.animals) {
            count += animal.count;
        }
        return count;
    }

    public static int GetNumberOfArtifacts() {
        int count = 0;
        foreach (var artifact in PlayerState.THIS.artifacts) {
            count += artifact.count;
        }
        return count;
    }

    public static List<InventoryAnimal> GetOrderedAnimals() {
        if (PlayerState.THIS.animals == null) return new List<InventoryAnimal>();
        // descending order according to value - sorted in place, may be redone later, if necessary
        PlayerState.THIS.animals.Sort((a1, a2) => a2.GetValue().CompareTo(a1.GetValue()));
        return PlayerState.THIS.animals;
    }

    public static List<InventoryArtifact> GetOrderedArtifacts() {
        if (PlayerState.THIS.artifacts == null)
            return new List<InventoryArtifact>();
        // descending order according to level of an expedition, where the artifact is a reward
        PlayerState.THIS.artifacts.Sort((a1, a2) => {
            int lvl1 = 0, lvl2 = 0;
            bool lvl1Found = false, lvl2Found = false;
            foreach (ExpeditionType expedition in GameLogic.THIS.expeditions) {
                Artifact artifact = expedition.reward;
                if (!lvl1Found && artifact == a1.artifact) {
                    lvl1 = expedition.level;
                    lvl1Found = true;
                }
                if (!lvl2Found && artifact == a2.artifact) {
                    lvl2 = expedition.level;
                    lvl2Found = true;
                }
                if (lvl1Found && lvl2Found) break;
            }
            return lvl2.CompareTo(lvl1);
        });
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

    public static bool TakeFromInventoryPrecise(Cost cost)
    {
        // first check - so that we don't pay part of cost before failure
        if (!HasInInventoryPrecise(cost))
            return false;
        foreach (InventoryAnimal animal in cost.animals)
        {
            TakeFromInventoryPrecise(animal);
        }
        foreach (InventoryArtifact artifact in cost.artifacts)
        {
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


    public static bool HasInInventoryPrecise(Cost cost)
    {
        foreach (InventoryAnimal animal in cost.animals)
        {
            if (!HasInInventoryPrecise(animal)) return false;
        }
        foreach (InventoryArtifact artifact in cost.artifacts)
        {
            if (!HasInInventory(artifact)) return false;
        }
        if (!HasInInventory(cost.resources)) return false;
        if (!HasInInventory(cost.money)) return false;
        return true;
    }


    public static void AddToInventory(InventoryAnimal animal) {
        var result = PlayerState.THIS.animals.Find(otherAnimal => animal == otherAnimal); // equality based on name, value and rarity
        if (result != null) result.count += animal.count;
        else PlayerState.THIS.animals.Add(animal);
    }

    public static void AddToInventory(InventoryArtifact artifact) {
        var result = PlayerState.THIS.artifacts.Find(otherArtifact => artifact == otherArtifact); // equality based on name
        if (result != null) result.count += artifact.count;
        else PlayerState.THIS.artifacts.Add(artifact);
    }

    public static void AddToInventory(List<EssenceAmount> essenceAmounts) {
        foreach (EssenceAmount essenceAmount in essenceAmounts) {
            AddToInventory(essenceAmount);
        }
    }

    public static void AddToInventory(EssenceAmount essenceAmount) {
        var result = PlayerState.THIS.resources.Find(otherEssence => essenceAmount.essence == otherEssence.essence);
        if (result != null) result.amount += essenceAmount.amount;
        else PlayerState.THIS.resources.Add(essenceAmount);
    }

    public static void AddToInventory(int money) {
        PlayerState.THIS.money += money;
    }



    public static void TakeFromInventory(InventoryAnimal animal) {
        List<InventoryAnimal> result = PlayerState.THIS.animals.FindAll(otherAnimal => animal <= otherAnimal); // the same name and greater or equal rarity
        if (result != null && HasInInventory(animal)) {
            result.Sort((r1, r2) => r1.rarity.CompareTo(r2.rarity));
            int cost = animal.count;
            for (int i = 0; i < result.Count; i++)
            {
                result[i].count -= cost;
                if (result[i].count <= 0) {
                    cost = -result[i].count;
                    PlayerState.THIS.animals.Remove(result[i]);
                }
                else
                {
                    break;
                }
            }
        }
    }

    public static void TakeFromInventoryPrecise(InventoryAnimal animal)
    {
        List<InventoryAnimal> result = PlayerState.THIS.animals.FindAll(otherAnimal => animal == otherAnimal); // the same name and equal rarity
        if (result != null && HasInInventory(animal))
        {
            result.Sort((r1, r2) => r1.rarity.CompareTo(r2.rarity));
            int cost = animal.count;
            for (int i = 0; i < result.Count; i++)
            {
                result[i].count -= cost;
                if (result[i].count <= 0)
                {
                    cost = -result[i].count;
                    PlayerState.THIS.animals.Remove(result[i]);
                }
                else
                {
                    break;
                }
            }
        }
    }

    public static void TakeFromInventory(InventoryArtifact artifact) {
        var result = PlayerState.THIS.artifacts.Find(otherArtifact => artifact.artifact == otherArtifact.artifact); // equality based on name
        if (result != null && HasInInventory(artifact)) {
            result.count -= artifact.count;
            if (result.count <= 0) PlayerState.THIS.artifacts.Remove(result);
        }
    }

    public static void TakeFromInventory(List<EssenceAmount> essenceAmounts) {
        if (HasInInventory(essenceAmounts))
        {
            foreach (EssenceAmount essenceAmount in essenceAmounts)
            {
                TakeFromInventory(essenceAmount);
            }
        }
    }

    public static void TakeFromInventory(EssenceAmount essenceAmount) {
        var result = PlayerState.THIS.resources.Find(otherEssence => essenceAmount.essence == otherEssence.essence);
        if (result != null && HasInInventory(essenceAmount)) {
            result.amount -= essenceAmount.amount;
            result.updateFull();
        }
    }

    public static void TakeFromInventory(int money) {
        if (HasInInventory(money))
            PlayerState.THIS.money -= money;
    }



    public static bool HasInInventory(InventoryAnimal animal) {
        List<InventoryAnimal> result = PlayerState.THIS.animals.FindAll(otherAnimal => animal <= otherAnimal); // the same animal at least rarity specified in the input
        int count = 0;
        foreach (InventoryAnimal ia in result)
        {
            count += ia.count;
        }
        return result != null && count >= animal.count;
    }

    public static bool HasInInventoryPrecise(InventoryAnimal animal)
    {
        List<InventoryAnimal> result = PlayerState.THIS.animals.FindAll(otherAnimal => animal == otherAnimal); // the same animal at least rarity specified in the input
        int count = 0;
        foreach (InventoryAnimal ia in result)
        {
            count += ia.count;
        }
        return result != null && count >= animal.count;
    }

    public static Rarity HighestRarityToPay(List<InventoryAnimal> animals)
    {
        Rarity highestRarity = Rarity.Common;
        foreach (InventoryAnimal animal in animals)
        {
            Rarity rarity = HighestRarityToPay(animal);
            if (rarity>highestRarity)
            {
                highestRarity = rarity;
            }
        }
        return highestRarity;
    }

    public static Rarity HighestRarityToPay(InventoryAnimal animal)
    {
        Rarity highestRarity = Rarity.Common;
        List<InventoryAnimal> result = PlayerState.THIS.animals.FindAll(otherAnimal => animal <= otherAnimal); // the same name and equal rarity
        if (result != null && HasInInventory(animal))
        {
            result.Sort((r1, r2) => r1.rarity.CompareTo(r2.rarity));
            int cost = animal.count;
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].count-cost < 0)
                {
                    cost = -result[i].count;
                    highestRarity = result[i].rarity;
                }
                else if (cost > 0)
                {
                    highestRarity = result[i].rarity;
                    break;
                }
                else
                {
                    break;
                }
            }
        }
        return highestRarity;
    }

    public static bool HasInInventory(InventoryArtifact artifact) {
        var result = PlayerState.THIS.artifacts.Find(otherArtifact => artifact.artifact == otherArtifact.artifact); // equality based on name
        return result != null && result.count >= artifact.count;
    }

    public static bool HasInInventory(List<EssenceAmount> essenceAmounts) {
        foreach (EssenceAmount essenceAmount in essenceAmounts) {
            if (!HasInInventory(essenceAmount)) return false;
        }
        return true;
    }

    public static bool HasInInventory(EssenceAmount essenceAmount) {
        var result = PlayerState.THIS.resources.Find(otherEssence => essenceAmount.essence == otherEssence.essence);
        return result != null && result.amount >= essenceAmount.amount;
    }

    public static bool HasInInventory(int money) {
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
public class InventoryAnimal {
    public Animal animal;
    public int count;
    public Rarity rarity;
    public bool locked = false;

    public InventoryAnimal() { }

    public InventoryAnimal(Animal animal, int count, Rarity rarity) {
        this.animal = animal;
        this.count = count;
        this.rarity = rarity;
    }

    public int GetValue() {
        return ((int)(animal.value * GameLogic.THIS.getRarityMultiplier(rarity) * PlayerState.THIS.recipes.Find(r => r.animal == animal).costMultiplier));
    }

    public float GetProbabilityOfDeath() {
        return GameLogic.THIS.GetRarityDeathProbability(this.rarity);
    }

    public int GetPower() {
        return (int)(this.animal.basePower * GameLogic.THIS.GetRarityPowerMultiplier(this.rarity));
    }

    public bool Equals(InventoryAnimal other) {
        if (other == null) return false;
        return this.animal == other.animal && rarity == other.rarity;
    }

    public bool GreaterOrEqual(InventoryAnimal other)
    {
        if (other == null) return false;
        return this.animal == other.animal && rarity>=other.rarity;
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

    public static bool operator >=(InventoryAnimal animal1, InventoryAnimal animal2)
    {
        if (((object)animal1) == null || ((object)animal2) == null)
            return object.Equals(animal1, animal2);
        return animal1.GreaterOrEqual(animal2);
    }

    public static bool operator <=(InventoryAnimal animal1, InventoryAnimal animal2)
    {
        if (((object)animal1) == null || ((object)animal2) == null)
            return object.Equals(animal1, animal2);
        return animal2.GreaterOrEqual(animal1);
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
public class InventoryArtifact
{
    public Artifact artifact;
    public int count;

    public InventoryArtifact() { }

    public InventoryArtifact(Artifact artifact, int count) {
        this.artifact = artifact;
        this.count = count;
    }

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
    Common = 0,
    Good = 1,
    Rare = 2,
    Epic = 3,
    Legendary = 4
}
