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
        int power = 0;
        foreach (PackSlot slot in this.slots) {
            if (slot.animal != null) power += slot.animal.GetPower();
        }
        return power;
    }
}

[System.Serializable]
public class PackSlot {
    [SerializeReference]
    public LocationType location;
    [SerializeReference]
    public InventoryAnimal animal;

    public PackSlot(LocationType location, InventoryAnimal animal) {
        this.location = location;
        this.animal = animal;
    }
}
