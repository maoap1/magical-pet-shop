using GoogleSheetsToUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProducersLoader : MonoBehaviour
{
    [MenuItem("Window/Spreadsheets/Load Producers")]
    public static void LoadExpeditions()
    {
        SpreadsheetManager manager = new SpreadsheetManager();
        SpreadsheetManager.Read(new GSTU_Search("1bAYSBOpD4GRwvvKtxxeVlcNhmVUJne-Abb28i65LqJk", "Sheet1"), ReadAndProcessProducers);
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
    => name != "" && name != "Level";

    public static void ReadAndProcessProducers(GstuSpreadSheet sheet)
    {
        List<EssenceProducerModel> existingProducers = GetExisting<EssenceProducerModel>();
        List<EssenceProducerLevel> levels = new List<EssenceProducerLevel>();
        foreach (var nameValue in sheet.columns["Level"])
        {
            string name = nameValue.value;
            if (NontrivialName(name))
            {
                EssenceProducerLevel level = new EssenceProducerLevel();
                level.productionRatePerMinute = int.Parse(sheet[name, "Production Rate"].value);
                level.cost = int.Parse(sheet[name, "Cost"].value);
                level.storageLimit = int.Parse(sheet[name, "Capacity"].value);
                levels.Add(level);
            }
        }
        foreach (var producer in existingProducers)
        {
            producer.producerLevels = levels;
            EditorUtility.SetDirty(producer);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
