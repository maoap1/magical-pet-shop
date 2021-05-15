using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Logic", menuName = "PetShop/Game Logic")]
public class Tutorials : ScriptableObject
{
    public List<ITutorial> tutorials;
    public int currentIndex = -1;
}
