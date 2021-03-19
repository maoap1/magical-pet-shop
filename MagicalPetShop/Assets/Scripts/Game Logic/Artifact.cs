using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Artifact", menuName = "PetShop/Artifact")]
public class Artifact : ScriptableObject, IEquatable<Artifact>
{
    public string name;
    public Sprite artwork;

    public bool Equals(Artifact other) {
        if (other == null) return false;
        return this.name == other.name;
    }

    public override bool Equals(object obj) {
        if (obj == null) return false;

        Artifact other = obj as Artifact;
        if (other == null) return false;
        else return Equals(other);
    }

    public override int GetHashCode() {
        return this.name.GetHashCode();
    }

    public static bool operator ==(Artifact artifact1, Artifact artifact2) {
        if (((object)artifact1) == null || ((object)artifact2) == null)
            return object.Equals(artifact1, artifact2);
        return artifact1.Equals(artifact2);
    }

    public static bool operator !=(Artifact artifact1, Artifact artifact2) {
        return !(artifact1 == artifact2);
    }
}
