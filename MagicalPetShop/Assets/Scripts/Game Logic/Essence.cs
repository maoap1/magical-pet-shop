using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Essence", menuName = "PetShop/Essence")]
public class Essence : ScriptableObject
{
    public Sprite icon;
    public string essenceName;
}

[Serializable]
public class EssenceAmount
{
    [SerializeReference]
    public Essence essence;
    public int amount;
    [HideInInspector]
    public bool full = false;

    public void IncreaseAmount(int increase)
    {
        amount += increase;
        if (amount>999)
        {
            amount = 999;
            full = true;
        }
        else
        {
            full = false;
        }
    }
}
