using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pack {
    // representation used in PlayerState (in contrast with PackLeader considers also assigned animals)
    public string name;
    public int level;
    public int cost;
    public Sprite artwork;
    public Sprite artworkSilhouette;
    public bool owned;
    public bool unlocked;
    [SerializeReference]
    public List<PackSlot> slots;
    public bool busy;

    public Pack(PackLeader leader) {
        this.name = leader.name;
        this.level = leader.level;
        this.cost = leader.cost;
        this.artwork = leader.artwork;
        this.owned = leader.owned;
        this.unlocked = leader.unlocked;
        this.busy = false;
        this.slots = new List<PackSlot>();
        foreach (LocationType location in leader.requiredLocations) {
            this.slots.Add(new PackSlot(location, null));
        }
    }

    public int GetTotalPower() {
        // TODO: Implement
        return 0;
    }
}

[System.Serializable]
public class PackSlot {
    [SerializeReference]
    public LocationType location;
    [SerializeReference]
    public Animal animal;

    public PackSlot(LocationType location, Animal animal) {
        this.location = location;
        this.animal = animal;
    }
}
