using GoogleSheetsToUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExpeditionsLoader : MonoBehaviour
{
    [MenuItem("Window/Spreadsheets/Load Expeditions")]
    public static void LoadExpeditions()
    {
        SpreadsheetManager manager = new SpreadsheetManager();
        SpreadsheetManager.Read(new GSTU_Search("192Ulm8VxwvgfVrLUKymARgfjtKovfOCL0anexl1SWNc", "Sheet1"), ReadAndProcessExpeditions);
    }

    private static List<T> GetExisting<T>() where T : class
    {
        string[] existingPaths = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
        List<T> existingList = new List<T>();
        foreach (string path in existingPaths)
        {
            existingList.Add(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(path), typeof(T)) as T);
        }
        return existingList;
    }

    private static bool NontrivialName(string name)
    => name != "" && name != "Expedition";

    private static bool ExpeditionExists(string name, List<ExpeditionType> existingExpeditions)
    {
        return existingExpeditions.Find(e => e.name == name) != null;
    }

    public static void ReadAndProcessExpeditions(GstuSpreadSheet sheet)
    {
        List<ExpeditionType> existingExpeditions = GetExisting<ExpeditionType>();
        List<Artifact> existingArtifacts = GetExisting<Artifact>();
        foreach (var nameValue in sheet.columns["Expedition"])
        {
            string name = nameValue.value;
            if (NontrivialName(name) && !ExpeditionExists(name, existingExpeditions))
            {
                ExpeditionType so = ScriptableObject.CreateInstance<ExpeditionType>();
                so.name = name;
                AssetDatabase.CreateAsset(so, "Assets/Expeditioning/Expeditions/" + so.name + ".asset");
                existingExpeditions.Add(so);
            }
        }
        foreach (var nameValue in sheet.columns["Expedition"])
        {
            string name = nameValue.value;
            if (ExpeditionExists(name, existingExpeditions))
            {
                ExpeditionType expedition = existingExpeditions.Find(e => e.name == name);
                expedition.duration = int.Parse(sheet[name, "Duration"].value);
                expedition.level = int.Parse(sheet[name, "Level"].value);
                expedition.reward = existingArtifacts.Find(a => a.name == sheet[name, "Artifact"].value);
                expedition.difficultyModes = new List<ExpeditionMode>();
                ExpeditionMode emEasy = new ExpeditionMode();

                emEasy.difficulty = ExpeditionDifficulty.Easy;
                emEasy.maxRewardCount = 6;
                emEasy.minRewardCount = 3;
                int basePower = int.Parse(sheet[name, "Easy"].value);
                emEasy.requiredPower.zeroPercent = (int)(0.9 * basePower);
                emEasy.requiredPower.fortyPercent = (int)(1.0 * basePower);
                emEasy.requiredPower.basePower = (int)(1.2 * basePower);
                expedition.difficultyModes.Add(emEasy);

                ExpeditionMode emMedium = new ExpeditionMode();
                emMedium.difficulty = ExpeditionDifficulty.Medium;
                emMedium.maxRewardCount = 12;
                emMedium.minRewardCount = 8;
                basePower = int.Parse(sheet[name, "Medium"].value);
                emMedium.requiredPower.zeroPercent = (int)(0.9 * basePower);
                emMedium.requiredPower.fortyPercent = (int)(1.0 * basePower);
                emMedium.requiredPower.basePower = (int)(1.2 * basePower);
                expedition.difficultyModes.Add(emMedium);

                ExpeditionMode emHard = new ExpeditionMode();
                emHard.difficulty = ExpeditionDifficulty.Hard;
                emHard.maxRewardCount = 21;
                emHard.minRewardCount = 15;
                basePower = int.Parse(sheet[name, "Hard"].value);
                emHard.requiredPower.zeroPercent = (int)(0.9 * basePower);
                emHard.requiredPower.fortyPercent = (int)(1.0 * basePower);
                emHard.requiredPower.basePower = (int)(1.2 * basePower);
                expedition.difficultyModes.Add(emHard);
                EditorUtility.SetDirty(expedition);
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}