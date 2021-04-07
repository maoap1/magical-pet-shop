using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Expeditioning
{
    /// TODO
    private static int numberOfSlots; // how many concurrent expeditions are possible

    /// tests if a new expedition can be started
    public static bool CanStartExpedition() {
        return false;
    }

    // try to start a specific expedition
    public static bool StartExpedition(Pack pack, ExpeditionType expeditionType, ExpeditionDifficulty difficulty) {
        return false;
    }

    // Try to finish expeditions the player's animals are currently on
    public static void TryFinishingExpeditions() { 
    
    }
}

public static class PacksManager {

    // TODO

    // Try to unlock a new leader (if there is enough money...)
    public static bool UnlockPack(Pack pack) {
        return false;
    }

    // Try to assign the given animal to the given slot in the given pack
    //      returns false in case of error (e.g. slotIndex does not correspond to the animal's abilities)
    public static bool AssignAnimal(Animal animal, Pack pack, int slotIndex) {
        Debug.Log("New animal assigned...");
        return false;
    }

    public static bool UnassignAnimal(Pack pack, int slotIndex) {
        Debug.Log("Old animal unassigned...");
        return false;
    }

    public static List<Pack> GetPacks() {
        return new List<Pack>();
    }
}

[Serializable]
public class Expedition
{
    public float fillRate;
    public Pack pack;
    public ExpeditionType expeditionType;
    public ExpeditionDifficulty difficulty;
}

public enum ExpeditionDifficulty { 
    Easy = 0,
    Medium = 1,
    Hard = 2
}

public class ExpeditionResult {
    Artifact reward;
    int rewardCount;
    List<Animal> casualties;
}
