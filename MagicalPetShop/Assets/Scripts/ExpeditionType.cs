using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Expedition Type", menuName = "PetShop/Expedition Type")]
public class ExpeditionType : ScriptableObject
{
    public int level;
    public int duration;
    public List<LocationType> Locations;
    public List<InventoryArtifact> rewards;
}
