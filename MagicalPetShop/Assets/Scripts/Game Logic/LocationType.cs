using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Location Type", menuName = "PetShop/Location Type")]
public class LocationType : ScriptableObject
{
    public string name;
    public Sprite artwork;

    public bool Equals(LocationType other)
    {
        if (other == null) return false;
        return this.name == other.name;
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;

        LocationType other = obj as LocationType;
        if (other == null) return false;
        else return Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        { // overflow does not matter
            int hash = 17;
            hash = hash * 23 + this.name.GetHashCode();
            return hash;
        }
    }

    public static bool operator ==(LocationType location1, LocationType location2)
    {
        if (((object)location1) == null || ((object)location2) == null)
            return object.Equals(location1, location2);
        return location1.Equals(location2);
    }

    public static bool operator !=(LocationType location1, LocationType location2)
    {
        return !(location1 == location2);
    }
}