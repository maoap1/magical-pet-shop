using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Essence", menuName = "PetShop/Essence")]
public class Essence : ScriptableObject
{
    public (string, int) essenceCount;
}
