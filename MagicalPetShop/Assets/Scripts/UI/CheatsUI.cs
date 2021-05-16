using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CheatsUI : MonoBehaviour
{

    void Awake() {
        CheatsUI[] objs = GameObject.FindObjectsOfType<CheatsUI>();
        if (objs.Length > 1) {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void ToggleVisibility() {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    private int width = 200;
    private int height = 100;
    private int offsetX = 0;
    private int offsetY = 10;

    private bool restart = false;

    private int ColumnToX(int column)
        => offsetX + column * width;

    private int RowToY(int row)
        => offsetY + row * height;

    private void OnGUI() {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        

        
        
        GUI.skin.button.fontSize = 35;

        // Add 10 000 money
        if (GUI.Button(new Rect(ColumnToX(0), RowToY(0), width, height), "Money")) {
            Inventory.AddToInventory(10000);
        }
        // Instantly give maximum level for all the recipes we have
        if (GUI.Button(new Rect(ColumnToX(0), RowToY(1), width, height), "Recipes")) {
            int recipesCount = PlayerState.THIS.recipes.Count;
            for (int i = 0; i < recipesCount; ++i) {
                RecipeProgress recipe = PlayerState.THIS.recipes[i];
                int count = recipe.recipe.recipeLevels[recipe.recipe.recipeLevels.Count - 1].treshold;
                count -= recipe.animalsProduced;
                for (int j = 0; j < count; ++j) recipe.animalProduced();
            }
        }
        // Full essence collectors
        if (GUI.Button(new Rect(ColumnToX(1), RowToY(0), width, height), "Essences")) {
            foreach (EssenceAmount essence in PlayerState.THIS.resources) {
                essence.IncreaseAmount(essence.limit);
            }
        }
        // Finish all expeditions
        if (GUI.Button(new Rect(ColumnToX(1), RowToY(1), width, height), "Expeditions")) {
            foreach (Expedition expedition in PlayerState.THIS.expeditions) {
                expedition.fillRate = 1;
            }
        }
        // Finish all crafting
        if (GUI.Button(new Rect(ColumnToX(2), RowToY(0), width, height), "Crafting")) {
            foreach (CraftedAnimal craftedAnimal in PlayerState.THIS.crafting) {
                if (craftedAnimal.isRecipe) craftedAnimal.fillRate = 1;
            }
        }
        // Finish all merging
        if (GUI.Button(new Rect(ColumnToX(2), RowToY(1), width, height), "Merging")) {
            foreach (CraftedAnimal craftedAnimal in PlayerState.THIS.crafting) {
                if (!craftedAnimal.isRecipe) craftedAnimal.fillRate = 1;
            }
        }
        // Restart progress

        if (GUI.Button(new Rect(ColumnToX(2), RowToY(2), width, height), "Restart")) {
            restart = true;
            
        }
        // Normal speed
        if (GUI.Button(new Rect(ColumnToX(3), RowToY(0), width, height), "x1")) {
            GameLogic.THIS.SetSpeed(1);
        }
        // x10 speed
        if (GUI.Button(new Rect(ColumnToX(3), RowToY(1), width, height), "x10")) {
            GameLogic.THIS.SetSpeed(10);
        }
        // 10x speed
        if (GUI.Button(new Rect(ColumnToX(3), RowToY(2), width, height), "x100")) {
            GameLogic.THIS.SetSpeed(100);
        }
        // Adds 10 of each artifact
        if (GUI.Button(new Rect(ColumnToX(0), RowToY(2), width, height), "Artifacts"))
        {
            AddArtifacts(10);
        }
        // Adds 10 of each discovered animal
        if (GUI.Button(new Rect(ColumnToX(1), RowToY(2), width, height), "Animals"))
        {
            AddAnimals(10);
        }


        if (restart)
        {
            if(GUI.Button(new Rect(ColumnToX(1), RowToY(6), 1.5f*width, 2*height), "NO"))
            {
                restart = false;
            }
            if(GUI.Button(new Rect(ColumnToX(3), RowToY(6), 1.5f*width, 2*height), "YES Restart"))
            {
                restart = false;
                PlayerState.THIS.LoadFromGameLogic();
            }
        }


        

#endif
    }

    private void AddArtifacts(int number)
    {
        List<Artifact> artifacts = new List<Artifact>();
        foreach (ExpeditionType ET in GameLogic.THIS.expeditions)
        {
            Artifact a = ET.reward;
            if (artifacts.Find(artifact => a.name == artifact.name) == null)
            {
                artifacts.Add(a);
            }
        }
        foreach (Artifact a in artifacts)
        {
            InventoryArtifact ia = new InventoryArtifact();
            ia.artifact = a;
            ia.count = number;
            Inventory.AddToInventory(ia);
        }
    }

    private void AddAnimals(int number)
    {
        List<RecipeProgress> RPs = PlayerState.THIS.recipes;
        int recipesCount = PlayerState.THIS.recipes.Count;
        for (int i=0; i<recipesCount; i++)
        {
            RecipeProgress rp = PlayerState.THIS.recipes[i];
            InventoryAnimal ia = new InventoryAnimal();
            ia.count = number;
            ia.animal = rp.animal;
            ia.rarity = rp.rarity;
            for (int j=0; j<number; j++)
            {
                rp.animalProduced();
            }
            Inventory.AddToInventory(ia);
        }
    }
}
