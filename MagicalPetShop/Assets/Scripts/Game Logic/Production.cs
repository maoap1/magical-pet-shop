using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase.Analytics;

[Serializable]
public class EssenceProducer
{
    public int level;
    public float productionRate
    {
        get { return model.GetProductionRate(level); }
    }
    public int storageLimit
    {
        get { return model.GetStorageLimit(level); }
    }
    [SerializeReference]
    public EssenceAmount essenceAmount;
    [Tooltip("Percent fill rate")]
    public float fillRate;
    [SerializeReference]
    public EssenceProducerModel model;
    public EssenceProducer(EssenceProducerModel model)
    {
        this.level = 0;
        this.essenceAmount = new EssenceAmount();
        this.essenceAmount.amount = 0;
        this.essenceAmount.essence = model.essence;
        this.fillRate = 0;
        this.model = model;
    }
    public EssenceProducer(EssenceProducerModel model, int level)
    {
        this.level = level;
        this.essenceAmount = new EssenceAmount();
        this.essenceAmount.amount = 0;
        this.essenceAmount.essence = model.essence;
        this.fillRate = 0;
        this.model = model;
    }

    public void UpgradeProducer()
    {
        int upgradeCost = model.GetCost(level + 1);
        Inventory.TakeFromInventory(upgradeCost);
        this.level += 1;
        PlayerState.THIS.Save();
        Analytics.LogEvent("producer_upgraded", new Parameter("essence", this.model.essence.essenceName), new Parameter("producer_level", this.level));
    }
}
