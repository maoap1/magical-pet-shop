using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Essence Producer Model", menuName = "PetShop/Essence Producer Model")]
public class EssenceProducerModel : ScriptableObject
{
    public Essence essence;
    public List<EssenceProducerLevel> producerLevels;
    public int GetProductionRate(int level) { return producerLevels[level].productionRate; }
    public Cost GetCost(int level) { return producerLevels[level].cost; }
}

[System.Serializable]
public class EssenceProducerLevel
{
    [Tooltip("Cost to buy this level")]
    public Cost cost;
    [Tooltip("Units per minute")]
    public int productionRate;
}
