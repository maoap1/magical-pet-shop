using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "PetShop/Recipe")]
public class Recipe : ScriptableObject
{
    public Animal animal;
    public Cost cost;
    [Tooltip("Duration in seconds")]
    public int duration;
    public int progression;
}
