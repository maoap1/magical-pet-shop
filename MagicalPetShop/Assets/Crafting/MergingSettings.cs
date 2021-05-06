using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using System.IO;
#endif

[CreateAssetMenu(fileName = "Merging Settings", menuName = "PetShop/Merging Settings")]
public class MergingSettings : ScriptableObject
{
    public MergingLevel[] mergingLevels = new MergingLevel[6];
    void OnValidate()
    {
        if (mergingLevels.Length != 6)
        {
            Debug.LogWarning("Don't change the 'mergingLevels' field's array size!");
            System.Array.Resize(ref mergingLevels, 6);
        }
    }

    [ContextMenu("Load from CSV")]
    void LoadFromCSV()
    {
#if UNITY_EDITOR
        MergingLoader.LoadMergingSettings();
#endif
    }
}
[System.Serializable]
public class MergingLevel
{
    public RarityMergingSettings[] rarityMergingSettings = new RarityMergingSettings[4];

    void OnValidate()
    {
        if (rarityMergingSettings.Length != 4)
        {
            Debug.LogWarning("Don't change the 'rarityMergingSettings' field's array size!");
            System.Array.Resize(ref rarityMergingSettings, 4);
        }
    }
}
[System.Serializable]
public class RarityMergingSettings
{
    public int artifactCost;
    public int duration;
}
