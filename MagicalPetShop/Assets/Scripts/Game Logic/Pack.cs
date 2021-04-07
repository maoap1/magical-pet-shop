using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pack", menuName = "PetShop/Pack")]
public class Pack : ScriptableObject {
    public string name;
    public int level;
    public int cost;
    public Sprite artwork;
    public bool owned;
    public List<LocationType> requiredLocations;
    public List<Animal> members;

    public int GetTotalPower() {
        // TODO: Implement
        return 0;
    }
}
