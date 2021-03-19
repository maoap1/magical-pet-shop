using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EssenceProducer
{
    public int level;
    [Tooltip("Units per minute")]
    public int productionRate;
    public int essenceAmount;
    public string essenceName;
    [NonSerialized]
    public Sprite essenceIcon;
    [Tooltip("Percent fill rate")]
    public float fillRate;
    [NonSerialized]
    public EssenceProducerModel model;
    public EssenceProducer(EssenceProducerModel model)
    {
        this.level = 0;
        this.productionRate = model.GetProductionRate(0);
        this.essenceAmount = 0;
        this.essenceName = model.essence.essenceName;
        this.essenceIcon = model.essence.icon;
        this.fillRate = 0;
        this.model = model;
    }
    public EssenceProducer(EssenceProducerModel model, int level)
    {
        this.level = level;
        this.productionRate = model.GetProductionRate(level);
        this.essenceAmount = 0;
        this.essenceName = model.essence.essenceName;
        this.essenceIcon = model.essence.icon;
        this.fillRate = 0;
        this.model = model;
    }

    public void UpgradeProducer()
    {
        Cost upgradeCost = model.GetCost(level + 1);
        PlayerState.THIS.money -= upgradeCost.money;
        this.level += 1;
        this.productionRate = model.GetProductionRate(level);
    }
}
