using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Expeditioning
{

    // tests if a new expedition can be started
    public static bool CanStartExpedition(Pack pack) {
        return pack.owned && !pack.busy;
    }

    // try to start a specific expedition
    public static bool StartExpedition(Pack pack, ExpeditionType expeditionType, ExpeditionDifficulty difficulty) {
        if (pack.owned && !pack.busy) {
            pack.busy = true;
            PlayerState.THIS.expeditions.Add(new Expedition(expeditionType, difficulty, pack));
            PlayerState.THIS.Save();
            return true;
        }
        return false;
    }

    // calculates result of the expedition (number of artifacts, casualties) and applies all necessary consequences
    public static ExpeditionResult FinishExpedition(Expedition expedition) {
        Random.InitState(System.DateTime.Now.Millisecond);
        // determine, if success
        bool isSuccessful = DetermineIfSuccessful(expedition);
        // determine number of rewards
        ExpeditionMode mode = expedition.expeditionType.difficultyModes[(int)expedition.difficulty];
        int rewardCount = 0;
        if (isSuccessful) {
            rewardCount = Random.Range(mode.minRewardCount, mode.maxRewardCount);
        }
        // determine casualties
        List<InventoryAnimal> casualties = ProcessCasualties(expedition, isSuccessful);
        // update everything
        InventoryArtifact reward = new InventoryArtifact(expedition.expeditionType.reward, rewardCount);
        if (rewardCount > 0) {
            Inventory.AddToInventory(reward);
        }
        foreach (Pack pack in PacksManager.GetPacks()) {
            if (pack.name == expedition.pack.name)
                pack.busy = false; // setting it directly to expedition.pack.busy does not work with saving
        }
        PlayerState.THIS.expeditions.Remove(expedition);
        PlayerState.THIS.Save();
        return new ExpeditionResult(isSuccessful, expedition.pack, reward, casualties);
    }

    private static bool DetermineIfSuccessful(Expedition expedition) {
        Random.InitState(System.DateTime.Now.Millisecond);
        double successChance = 0;
        int packPower = expedition.pack.GetTotalPower();
        ExpeditionMode mode = expedition.expeditionType.difficultyModes[(int)expedition.difficulty];
        if (packPower < mode.requiredPower.zeroPercent) {
            successChance = 0;
        } else if (packPower < mode.requiredPower.fortyPercent) {
            successChance = 0.1;
        } else if (packPower < mode.requiredPower.basePower) {
            successChance = ((double)(packPower - mode.requiredPower.fortyPercent) / (mode.requiredPower.basePower - mode.requiredPower.fortyPercent));
            successChance *= 0.6;
            successChance += 0.4;
        } else {
            successChance = 1;
        }
        return Random.value < successChance;
    }

    private static List<InventoryAnimal> ProcessCasualties(Expedition expedition, bool isSuccessful) {
        Random.InitState(System.DateTime.Now.Millisecond);
        // for each animal, determine life/death
        float averageProbOfDeath = 0;
        int count = 0;
        List<PackSlot> casualties = new List<PackSlot>();
        foreach (PackSlot slot in expedition.pack.slots) {
            if (slot.animal != null) {
                float prob = slot.animal.GetProbabilityOfDeath();
                averageProbOfDeath += prob;
                ++count;
                if (Random.value < prob) casualties.Add(slot);
            }
        }
        if (count == 0) return new List<InventoryAnimal>();

        averageProbOfDeath /= count;
        // if too many animals are destined to die, choose only some of them
        int maxCasualties = 1;
        for (int i = 0; i < GameLogic.THIS.casualtiesThreshold.Length; ++i) {
            if (averageProbOfDeath <= GameLogic.THIS.casualtiesThreshold[i]) break;
            ++maxCasualties;
        }
        if (isSuccessful && maxCasualties >= count) maxCasualties = count - 1; // at least one animal must survive, if the expedition is successful
        for (int i = maxCasualties; i < casualties.Count; i++) {
            casualties.RemoveAt(Random.Range(0, casualties.Count - 1));
        }
        List<InventoryAnimal> animalCasualties = new List<InventoryAnimal>();
        foreach (PackSlot slot in casualties)
            animalCasualties.Add(slot.animal);
        // apply changes
        PacksManager.ManageCasualties(casualties);

        return animalCasualties;
    }
}

public static class PacksManager {

    // Try to buy a new leader (if there is enough money...)
    public static bool BuyPack(Pack pack) {
        if (pack.unlocked && !pack.owned && Inventory.HasInInventory(pack.cost)) {
            Inventory.TakeFromInventory(pack.cost);
            pack.owned = true;
            PlayerState.THIS.Save();
            return true;
        }
        return false;
    }

    // Try to unlock new leaders
    public static bool UnlockPacks() {
        bool newPackUnlocked = false;
        foreach (Pack pack in PlayerState.THIS.packs) {
            if (!pack.unlocked && pack.level <= PlayerState.THIS.level) {
                pack.unlocked = true;
                newPackUnlocked = true;
            }
            else if (pack.level > PlayerState.THIS.level)
            {
                pack.unlocked = false;
                pack.owned = false;
            }
        }
        PlayerState.THIS.Save();
        return newPackUnlocked;
    }

    // Try to assign the given animal to the given slot in the given pack
    //      returns false in case of error
    public static bool AssignAnimal(InventoryAnimal animal, Pack pack, PackSlot slot) {
        // validate
        if (!pack.slots.Contains(slot)) return false;
        if (slot.location != animal.animal.category && !animal.animal.secondaryCategories.Contains(slot.location)) return false;
        // assign
        InventoryAnimal assignedAnimal = new InventoryAnimal(animal.animal, 1, animal.rarity);
        if (Inventory.HasInInventory(assignedAnimal)) {
            Inventory.TakeFromInventory(assignedAnimal);
            slot.animal = assignedAnimal;
            PlayerState.THIS.Save();
            return true;
        }
        return false;
    }

    public static bool UnassignAnimal(Pack pack, PackSlot slot) {
        // validate
        if (!pack.slots.Contains(slot)) return false;
        if (slot.animal == null) return false;
        // unassign
        Inventory.AddToInventory(slot.animal);
        slot.animal = null;
        PlayerState.THIS.Save();
        return true;
    }

    public static void ManageCasualties(List<PackSlot> casualties) {
        foreach (PackSlot slot in casualties) {
            slot.animal = null;
        }
    }

    public static List<Pack> GetPacks() {
        if (PlayerState.THIS.packs == null) return new List<Pack>();
        else return PlayerState.THIS.packs;
    }
}

[System.Serializable]
public class Expedition
{
    public float fillRate;
    public ExpeditionType expeditionType;
    public ExpeditionDifficulty difficulty;
    public Pack pack;

    public Expedition(ExpeditionType expedition, ExpeditionDifficulty difficulty, Pack pack) {
        this.fillRate = 0;
        this.expeditionType = expedition;
        this.difficulty = difficulty;
        this.pack = pack;
    }
}

public enum ExpeditionDifficulty { 
    Easy = 0,
    Medium = 1,
    Hard = 2
}

public class ExpeditionResult {
    public bool isSuccessful;
    public Pack pack;
    public InventoryArtifact reward;
    public List<InventoryAnimal> casualties;

    public ExpeditionResult(bool success, Pack pack, InventoryArtifact reward, List<InventoryAnimal> casualties) {
        this.isSuccessful = success;
        this.pack = pack;
        this.reward = reward;
        this.casualties = casualties;
    }
}
