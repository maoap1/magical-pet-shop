using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Essence", menuName = "PetShop/Essence")]
public class Essence : ScriptableObject, IEquatable<Essence>
{
    public (string, int) essenceCount;

    public bool Equals(Essence other) {
        if (other == null) return false;
        return this.essenceCount.Item1 == other.essenceCount.Item1;
    }

    public override bool Equals(object obj) {
        if (obj == null) return false;

        Essence other = obj as Essence;
        if (other == null) return false;
        else return Equals(other);
    }

    public override int GetHashCode() {
        return this.essenceCount.Item1.GetHashCode();
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
