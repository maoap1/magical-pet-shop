using GoogleSheetsToUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PackLeadersLoader : MonoBehaviour
{
    [MenuItem("Window/Spreadsheets/Load Pack Leaders")]
    public static void LoadMergingSettings()
    {
        SpreadsheetManager manager = new SpreadsheetManager();
        SpreadsheetManager.Read(new GSTU_Search("1ZtekHFyLpwcgJvtXZnUa1VfUN6qa-hN3grObNh49vBY", "Sheet1"), ReadAndProcessPackLeaders);
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
    => name != "" && name != "Pack Leader";

    private static bool PackLeaderExists(string name, List<PackLeader> existingPackLeaders)
    {
        return existingPackLeaders.Find(l => l.name == name) != null;
    }

    public static void ReadAndProcessPackLeaders(GstuSpreadSheet sheet)
    {
        List<PackLeader> existingPackLeaders = GetExisting<PackLeader>();
        List<LocationType> existingLocations = GetExisting<LocationType>();
        foreach (var nameValue in sheet.columns["Pack Leader"])
        {
            string name = nameValue.value;
            if (NontrivialName(name) && !PackLeaderExists(name, existingPackLeaders))
            {
                PackLeader so = ScriptableObject.CreateInstance<PackLeader>();
                so.name = name;
                AssetDatabase.CreateAsset(so, "Assets/Expeditioning/Leaders/" + so.name + ".asset");
                existingPackLeaders.Add(so);
            }
        }
        foreach (var nameValue in sheet.columns["Pack Leader"])
        {
            string name = nameValue.value;
            if (PackLeaderExists(name, existingPackLeaders))
            {
                PackLeader packLeader = existingPackLeaders.Find(pl => pl.name == name);
                packLeader.owned = false;
                packLeader.unlocked = false;
                packLeader.cost = int.Parse(sheet[name, "Cost"].value);
                packLeader.level = int.Parse(sheet[name, "Level"].value);
                packLeader.requiredLocations = new List<LocationType>();
                packLeader.requiredLocations.Add(existingLocations.Find(l => l.name == sheet[name, "Item 1"].value));
                packLeader.requiredLocations.Add(existingLocations.Find(l => l.name == sheet[name, "Item 2"].value));
                packLeader.requiredLocations.Add(existingLocations.Find(l => l.name == sheet[name, "Item 3"].value));
                packLeader.requiredLocations.Add(existingLocations.Find(l => l.name == sheet[name, "Item 4"].value));
                packLeader.requiredLocations.Add(existingLocations.Find(l => l.name == sheet[name, "Item 5"].value));
                packLeader.requiredLocations.Add(existingLocations.Find(l => l.name == sheet[name, "Item 6"].value));
                EditorUtility.SetDirty(packLeader);
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
