using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PackLeader", menuName = "PetShop/Pack Leader")]
public class PackLeader : ScriptableObject {
    // representation used in GameLogic, created by designer
    public string name;
    public int level;
    public int cost;
    public Sprite artwork;
    public bool owned;
    public bool unlocked;
    public List<LocationType> requiredLocations;
}