using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExpeditionMode", menuName = "PetShop/Expedition Mode")]
public class ExpeditionMode : ScriptableObject {
    public ExpeditionDifficulty difficulty;
    public int minRewardCount;
    public int maxRewardCount;
}
