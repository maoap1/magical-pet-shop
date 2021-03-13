using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Animal", menuName = "PetShop/Animal")]
public class Animal : ScriptableObject
{
    public string name;
    public int level;
    public Rarity rarity;
    public int value;
    public LocationType category;
    public List<LocationType> secondaryCategories;
    public Sprite artwork;
}
