using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Expedition Type", menuName = "PetShop/Expedition Type")]
public class ExpeditionType : ScriptableObject
{
    public int level;
    public int duration; // maybe more reasonable type for representing time and suitable ToString implementation?
    public Artifact reward;
    public Sprite artwork;
    public List<ExpeditionMode> expeditionModes; // different difficulties
}

[CreateAssetMenu(fileName = "Expedition Mode", menuName = "PetShop/Expedition Mode")]
public class ExpeditionMode : ScriptableObject {
    public ExpeditionDifficulty difficulty;
    public int minRewardCount;
    public int maxRewardCount;
}


