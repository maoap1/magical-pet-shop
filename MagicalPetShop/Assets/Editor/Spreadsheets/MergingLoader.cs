using GoogleSheetsToUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MergingLoader : MonoBehaviour
{
    [MenuItem("Window/Spreadsheets/Load Merging")]
    public static void LoadMergingSettings()
    {
        SpreadsheetManager manager = new SpreadsheetManager();
        SpreadsheetManager.Read(new GSTU_Search("1Fj-oMx8JXLWusfv2QUYYIGD_JIpVbBiK-IA4Crc-R-Y", "Sheet1"), ReadAndProcessMerging);
    }

    public static void ReadAndProcessMerging(GstuSpreadSheet sheet)
    {
        MergingSettings ms = Resources.Load<MergingSettings>("Merging Settings");
        for (int i = 0; i < ms.mergingLevels.Length; i++) {
            string columnName = "Tier " + (i + 1);
            ms.mergingLevels[i].rarityMergingSettings[0].artifactCost = int.Parse(sheet["C->G", columnName].value);
            ms.mergingLevels[i].rarityMergingSettings[1].artifactCost = int.Parse(sheet["G->R", columnName].value);
            ms.mergingLevels[i].rarityMergingSettings[2].artifactCost = int.Parse(sheet["R->E", columnName].value);
            ms.mergingLevels[i].rarityMergingSettings[3].artifactCost = int.Parse(sheet["E->L", columnName].value);

            ms.mergingLevels[i].rarityMergingSettings[0].duration = int.Parse(sheet["C->G Time", columnName].value);
            ms.mergingLevels[i].rarityMergingSettings[1].duration = int.Parse(sheet["G->R Time", columnName].value);
            ms.mergingLevels[i].rarityMergingSettings[2].duration = int.Parse(sheet["R->E Time", columnName].value);
            ms.mergingLevels[i].rarityMergingSettings[3].duration = int.Parse(sheet["E->L Time", columnName].value);
        }
    }
}
