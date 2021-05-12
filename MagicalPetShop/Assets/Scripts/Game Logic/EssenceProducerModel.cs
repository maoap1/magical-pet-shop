using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Essence Producer Model", menuName = "PetShop/Essence Producer Model")]
public class EssenceProducerModel : ScriptableObject
{
    public Essence essence;
    public List<EssenceProducerLevel> producerLevels;
    public float GetProductionRate(int level) { if (level < producerLevels.Count) { return producerLevels[level].productionRatePerMinute; } else { return -1; } }
    public int GetStorageLimit(int level) { if (level < producerLevels.Count) { return producerLevels[level].storageLimit; } else { return -1; } }
    public int GetCost(int level) { if (level < producerLevels.Count) { return producerLevels[level].cost; } else { return -1; } }
}

[System.Serializable]
public class EssenceProducerLevel
{
    [Tooltip("Cost to buy this level - money")]
    public int cost;
    [Tooltip("Units per minute")]
    public float productionRatePerMinute;
    public int storageLimit;
}
