using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Essence", menuName = "PetShop/Essence")]
public class Essence : ScriptableObject, IEquatable<Essence>
{
    public Sprite icon;
    public string essenceName;
    public LocationType associatedLocation;

    public bool Equals(Essence other) {
        if (other == null) return false;
        return this.essenceName == other.essenceName;
    }

    public override bool Equals(object obj) {
        if (obj == null) return false;

        Essence other = obj as Essence;
        if (other == null) return false;
        else return Equals(other);
    }

    public override int GetHashCode() {
        return this.essenceName.GetHashCode();
    }

    public static bool operator ==(Essence essence1, Essence essence2) {
        if (((object)essence1) == null || ((object)essence2) == null)
            return object.Equals(essence1, essence2);
        return essence1.Equals(essence2);
    }

    public static bool operator !=(Essence essence1, Essence essence2) {
        return !(essence1 == essence2);
    }
}


[Serializable]
public class EssenceAmount
{
    [SerializeReference]
    public Essence essence;
    [HideInInspector]
    public int limit;
    public int amount;
    [HideInInspector]
    public bool full = false;
    [HideInInspector]
    public bool unlocked;

    public void IncreaseAmount(int increase)
    {
        amount += increase;
        if (amount>=limit)
        {
            amount = limit;
            full = true;
        }
        else
        {
            full = false;
        }
    }

    public void SetLimit(int limit)
    {
        this.limit = limit;
    }
}
