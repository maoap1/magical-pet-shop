using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Crafting
{
    /// TODO
    /// tests if crafting can be started
    //public static bool CanStartCrafting(Recipe recipe) { }
    // try to start crafting
    //public static bool StartCrafting(Recipe recipe) { }
    // Try to finish crafting currently going on
    //public static void TryFinishingCrafting() { }
}

[Serializable]
public struct CraftedAnimal
{
    float fillRate;
    Recipe recipe;
}
