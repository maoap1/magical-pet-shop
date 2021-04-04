using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RecipeLevel))]
public class RecipeUpgradeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        RecipeUpgradeType upgradeType = (RecipeUpgradeType)serializedObject.FindProperty("upgradeType").intValue;
        Debug.Log(upgradeType);

        switch (upgradeType)
        {
            case RecipeUpgradeType.decreaseAnimals:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("costAnimalsDecrease"));
                break;
            case RecipeUpgradeType.decreaseEssences:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("costEssencesDecrease"));
                break;
            case RecipeUpgradeType.decreaseArtifacts:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("costArtifactsDecrease"));
                break;
        }
    }
}
