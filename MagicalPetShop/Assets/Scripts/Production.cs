using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct EssenceProducer
{
    public int level;
    [Tooltip("Units per minute")]
    public int productionRate;
    public string name;
    [Tooltip("Percent fill rate")]
    public float fillRate;
    //public static void UpdateResources() {}
    //public static bool CanUpgrade(string producerName) {}
    //public static bool Upgrade(string producerName) {}
}

[Serializable]
public struct EssenceProducers
{
    public List<EssenceProducer> producers;
}
