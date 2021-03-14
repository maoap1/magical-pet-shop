using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Expeditioning
{
    /// TODO
    /// tests if a new expedition can be started
    //public static bool CanStartExpedition();
    // try to start a specific expedition
    //public static bool StartExpedition(List<InventoryAnimal> animals, ExpedtionType expeditionType);
    // Try to finish expeditions the player's annimals are currently on
    //public static void TryFinishingExpeditions();
}

[Serializable]
public struct Expedition
{
    public float fillRate;
    public List<InventoryAnimal> animals;
    public ExpeditionType expeditionType;
}
