using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExpeditionMode", menuName = "PetShop/Expedition Mode")]
public class ExpeditionMode : ScriptableObject {
    public ExpeditionDifficulty difficulty;
    public List<int> requiredPowerIntervals; // intervals for success rate (< 0 %, 40-100 %, 100 %)
    public int minRewardCount;
    public int maxRewardCount;
}
