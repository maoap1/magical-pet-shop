using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExpeditionMode {
    public ExpeditionDifficulty difficulty;
    public RequiredPower requiredPower; // intervals for success rate (< 0 %, 40-100 %, 100 %)
    public int minRewardCount;
    public int maxRewardCount;
}

[System.Serializable]
public struct RequiredPower {
    public int basePower;
    public int zeroPercent; // everything below that is certain fail
    public int fortyPercent; // everything below that is < 40 % probability of success
}