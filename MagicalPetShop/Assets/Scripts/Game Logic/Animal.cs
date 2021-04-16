using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Animal", menuName = "PetShop/Animal")]
public class Animal : ScriptableObject
{
    public string name;
    public int level;
    public int value;
    public int basePower;
    public LocationType category;
    public List<LocationType> secondaryCategories;
    public Sprite artwork;
    public Artifact associatedArtifact;

    public bool Equals(Animal other) {
        if (other == null) return false;
        return this.name == other.name && this.value == other.value;
    }

    public override bool Equals(object obj) {
        if (obj == null) return false;

        Animal other = obj as Animal;
        if (other == null) return false;
        else return Equals(other);
    }

    public override int GetHashCode() {
        unchecked { // overflow does not matter
            int hash = 17;
            hash = hash * 23 + this.name.GetHashCode();
            hash = hash * 23 + this.value.GetHashCode();
            return hash;
        }
    }

    public static bool operator ==(Animal animal1, Animal animal2) {
        if (((object)animal1) == null || ((object)animal2) == null)
            return object.Equals(animal1, animal2);
        return animal1.Equals(animal2);
    }

    public static bool operator !=(Animal animal1, Animal animal2) {
        return !(animal1 == animal2);
    }
}
