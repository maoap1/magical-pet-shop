using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleSheetsToUnity;
using UnityEditor;
using System;
using System.Globalization;

public class RecipeLoader : MonoBehaviour
{
    [MenuItem("Window/Spreadsheets/Load Recipes")]
    public static void LoadRecipes()
    {
        SpreadsheetManager manager = new SpreadsheetManager();
        SpreadsheetManager.Read(new GSTU_Search("1JsFLaENkpnlaC3EvOYpIvN1iKYTAf6UtLDoidxWZVYI", "Sheet1"), ReadAndProcessRecipes);
    }

    private static void ReadAndProcessRecipes(GstuSpreadSheet sheet)
    {
        string[] existingAnimalsPaths = AssetDatabase.FindAssets(String.Format("t:{0}", typeof(Animal)));
        List<Animal> existingAnimals = new List<Animal>();
        foreach (string path in existingAnimalsPaths)
        {
            existingAnimals.Add(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(path), typeof(Animal)) as Animal);
        }

        string[] existingArtifactsPaths = AssetDatabase.FindAssets(String.Format("t:{0}", typeof(Artifact)));
        List<Artifact> existingArtifacts = new List<Artifact>();
        foreach (string path in existingArtifactsPaths)
        {
            existingArtifacts.Add(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(path), typeof(Artifact)) as Artifact);
        }

        string[] existingEssencesPaths = AssetDatabase.FindAssets(String.Format("t:{0}", typeof(Essence)));
        List<Essence> existingEssences = new List<Essence>();
        foreach (string path in existingEssencesPaths)
        {
            existingEssences.Add(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(path), typeof(Essence)) as Essence);
        }

        string[] existingRecipesPaths = AssetDatabase.FindAssets(String.Format("t:{0}", typeof(Recipe)));
        List<Recipe> existingRecipes = new List<Recipe>();
        foreach (string path in existingRecipesPaths)
        {
            existingRecipes.Add(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(path), typeof(Recipe)) as Recipe);
        }

        string[] existingLocationPaths = AssetDatabase.FindAssets(String.Format("t:{0}", typeof(LocationType)));
        List<LocationType> existingLocations = new List<LocationType>();
        foreach (string path in existingLocationPaths)
        {
            existingLocations.Add(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(path), typeof(LocationType)) as LocationType);
        }


        foreach (var nameValue in sheet.columns["Name"])
        {
            //Load artifacts
            if (nameValue.value != "" && nameValue.value != "Name" && existingArtifacts.Find(a => a.name != sheet[nameValue.value, "Artifact"].value) != null)
            {
                if (sheet[nameValue.value, "Artifact"].value != "---")
                {
                    Artifact so = ScriptableObject.CreateInstance<Artifact>();
                    so.name = sheet[nameValue.value, "Artifact"].value;
                    AssetDatabase.CreateAsset(so, "Assets/Artifacts/" + so.name.Replace("/", "").Replace("\\", "") + ".asset");
                    existingArtifacts.Add(so);
                }
            }
            /*
            if (nameValue.value != "" && nameValue.value != "Name" && existingArtifacts.Find(a => a.name != sheet[nameValue.value, "Merging artifact"].value) != null)
            {
                Artifact so = ScriptableObject.CreateInstance<Artifact>();
                so.name = sheet[nameValue.value, "Artifact"].value;
                AssetDatabase.CreateAsset(so, "Assets/Artifacts/" + so.name.Replace("/", "").Replace("\\", "") + ".asset");
                existingArtifacts.Add(so);
            }
            */
        }
        /*
        foreach (string path in existingRecipesPaths)
        {
            AssetDatabase.DeleteAsset(path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        */
        foreach (var nameValue in sheet.columns["Name"])
        {
            //Load animals
            if (nameValue.value != "" && nameValue.value != "Name" && existingAnimals.Find(a => a.name != nameValue.value) != null)
            {
                if (sheet[nameValue.value, "Animal"].value != "---")
                {
                    Animal so = ScriptableObject.CreateInstance<Animal>();
                    so.name = nameValue.value;
                    so.level = int.Parse(sheet[nameValue.value, "Tier"].value);
                    so.value = int.Parse(sheet[nameValue.value, "Value"].value.Replace(",", ""));
                    so.animalEssence = existingEssences.Find(e => e.name == sheet[nameValue.value, "Category"].value);
                    so.basePower = int.Parse(sheet[nameValue.value, "Power"].value.Replace(",", ""));
                    so.associatedArtifact = existingArtifacts.Find(a => a.name != sheet[nameValue.value, "Merging artifact"].value);
                    so.secondaryCategories = new List<LocationType>();
                    if (sheet[nameValue.value, "Secondary category"].value != "---")
                    {
                        so.secondaryCategories.Add(existingLocations.Find(l => l.name == sheet[nameValue.value, "Secondary category"].value));
                    }
                    AssetDatabase.CreateAsset(so, "Assets/Animals/" + so.name.Replace("/", "").Replace("\\", "") + ".asset");
                    existingAnimals.Add(so);
                }
            }
            else if (existingAnimals.Find(a => a.name == nameValue.value) != null)
            {
                Animal animal = existingAnimals.Find(a => a.name == nameValue.value);
                animal.level = int.Parse(sheet[nameValue.value, "Tier"].value);
                animal.value = int.Parse(sheet[nameValue.value, "Value"].value.Replace(",", ""));
                animal.animalEssence = existingEssences.Find(e => e.name == sheet[nameValue.value, "Category"].value);
                animal.basePower = int.Parse(sheet[nameValue.value, "Power"].value.Replace(",", ""));
                animal.associatedArtifact = existingArtifacts.Find(a => a.name != sheet[nameValue.value, "Merging artifact"].value);
                EditorUtility.SetDirty(animal);
            }
        }
        foreach (var nameValue in sheet.columns["Name"])
        {
            //Load recipes
            if (nameValue.value != "" && nameValue.value != "Name" && existingRecipes.Find(r => r.animal.name != nameValue.value) != null)
            {
                Recipe so = ScriptableObject.CreateInstance<Recipe>();
                so.animal = existingAnimals.Find(a => a.name == nameValue.value);
                AssetDatabase.CreateAsset(so, "Assets/Recipes/" + so.animal.name.Replace("/", "").Replace("\\", "") + ".asset");
                existingRecipes.Add(so);
            }
        }
        foreach (var nameValue in sheet.columns["Name"])
        {
            if (existingRecipes.Find(r => r.animal.name == nameValue.value) != null)
            {
                Recipe recipe = existingRecipes.Find(r => r.animal.name == nameValue.value);
                recipe.animal = existingAnimals.Find(a => a.name == nameValue.value);

                recipe.baseCostArtifacts = new List<InventoryArtifact>();


                if (sheet[nameValue.value, "Artifact"].value != "---")
                {
                    InventoryArtifact iar = new InventoryArtifact();
                    iar.count = int.Parse(sheet[nameValue.value, "Artifact Amount"].value);
                    iar.artifact = existingArtifacts.Find(a => a.name == sheet[nameValue.value, "Artifact"].value);
                    recipe.baseCostArtifacts.Add(iar);
                }

                recipe.baseCostAnimals = new List<InventoryAnimal>();
                if (sheet[nameValue.value, "Animal"].value != "---")
                {
                    InventoryAnimal ian = new InventoryAnimal();
                    ian.count = int.Parse(sheet[nameValue.value, "Animal Amount"].value);
                    ian.animal = existingAnimals.Find(a => a.name == sheet[nameValue.value, "Animal"].value);
                    recipe.baseCostAnimals.Add(ian);
                }


                //Essences
                recipe.baseCostEssences = new List<EssenceAmount>();
                if (sheet[nameValue.value, "Water Essence"].value != "---")
                {
                    EssenceAmount ea = new EssenceAmount();
                    ea.amount = int.Parse(sheet[nameValue.value, "Water Essence"].value);
                    ea.essence = existingEssences.Find(e => e.essenceName == "Water");
                    recipe.baseCostEssences.Add(ea);
                }
                if (sheet[nameValue.value, "Earth Essence"].value != "---")
                {
                    EssenceAmount ea = new EssenceAmount();
                    ea.amount = int.Parse(sheet[nameValue.value, "Earth Essence"].value);
                    ea.essence = existingEssences.Find(e => e.essenceName == "Earth");
                    recipe.baseCostEssences.Add(ea);
                }
                if (sheet[nameValue.value, "Fire Essence"].value != "---")
                {
                    EssenceAmount ea = new EssenceAmount();
                    ea.amount = int.Parse(sheet[nameValue.value, "Fire Essence"].value);
                    ea.essence = existingEssences.Find(e => e.essenceName == "Fire");
                    recipe.baseCostEssences.Add(ea);
                }
                if (sheet[nameValue.value, "Power Essence"].value != "---")
                {
                    EssenceAmount ea = new EssenceAmount();
                    ea.amount = int.Parse(sheet[nameValue.value, "Power Essence"].value);
                    ea.essence = existingEssences.Find(e => e.essenceName == "Power");
                    recipe.baseCostEssences.Add(ea);
                }
                if (sheet[nameValue.value, "Magic Essence"].value != "---")
                {
                    EssenceAmount ea = new EssenceAmount();
                    ea.amount = int.Parse(sheet[nameValue.value, "Magic Essence"].value);
                    ea.essence = existingEssences.Find(e => e.essenceName == "Magic");
                    recipe.baseCostEssences.Add(ea);
                }
                if (sheet[nameValue.value, "Ice Essence"].value != "---")
                {
                    EssenceAmount ea = new EssenceAmount();
                    ea.amount = int.Parse(sheet[nameValue.value, "Ice Essence"].value);
                    ea.essence = existingEssences.Find(e => e.essenceName == "Ice");
                    recipe.baseCostEssences.Add(ea);
                }

                recipe.baseDuration = int.Parse(sheet[nameValue.value, "Crafting Time (seconds)"].value.Replace(",", ""));
                recipe.baseRarity = Rarity.Common;
                //Recipe levels
                recipe.recipeLevels = new List<RecipeLevel>();
                recipe.recipeLevels.Add(parseRecipeLevel(sheet[nameValue.value, "Crafting Upgrade 1"].value, int.Parse(sheet[nameValue.value, "Crafts Needed 1"].value), existingEssences, existingArtifacts, existingAnimals, existingRecipes));
                recipe.recipeLevels.Add(parseRecipeLevel(sheet[nameValue.value, "Crafting Upgrade 2"].value, int.Parse(sheet[nameValue.value, "Crafts Needed 2"].value), existingEssences, existingArtifacts, existingAnimals, existingRecipes));
                recipe.recipeLevels.Add(parseRecipeLevel(sheet[nameValue.value, "Crafting Upgrade 3"].value, int.Parse(sheet[nameValue.value, "Crafts Needed 3"].value), existingEssences, existingArtifacts, existingAnimals, existingRecipes));
                recipe.recipeLevels.Add(parseRecipeLevel(sheet[nameValue.value, "Crafting Upgrade 4"].value, int.Parse(sheet[nameValue.value, "Crafts Needed 4"].value), existingEssences, existingArtifacts, existingAnimals, existingRecipes));
                recipe.recipeLevels.Add(parseRecipeLevel(sheet[nameValue.value, "Crafting Upgrade 5"].value, int.Parse(sheet[nameValue.value, "Crafts Needed 5"].value), existingEssences, existingArtifacts, existingAnimals, existingRecipes));
                EditorUtility.SetDirty(recipe);
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        //UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(this.gameObject.scene);
    }

    private static RecipeLevel parseRecipeLevel(string description, int treshold, List<Essence> existingEssences, List<Artifact> existingArtifacts, List<Animal> existingAnimals, List<Recipe> existingRecipes)
    {
        RecipeLevel output = new RecipeLevel();
        output.treshold = treshold;
        string[] parts = description.Split(':');
        string value = parts[1];
        if (parts[0] == "Craft Time Reduction")
        {
            output.upgradeType = RecipeUpgradeType.decreaseDuration;
            value = value.TrimStart(new char[] { ' ', '+', '-' });
            value = value.TrimEnd(new char[] { '%' });
            output.durationDecrease = int.Parse(value);
        }
        else if (parts[0] == "Value Increase")
        {
            output.upgradeType = RecipeUpgradeType.increaseValue;
            value = value.TrimStart(new char[] { ' ', '+', '-' });
            value = value.TrimEnd(new char[] { '%' });
            output.valueIncrease = int.Parse(value);
        }
        else if (parts[0] == "Essences")
        {
            output.upgradeType = RecipeUpgradeType.decreaseEssences;
            value = value.TrimStart(new char[] { ' ', '-' });
            string[] valueParts = value.Split(new char[] { ' ' }, 2);

            if (valueParts[1] == "EoW")
            {
                EssenceAmount ea = new EssenceAmount();
                ea.amount = int.Parse(valueParts[0]);
                ea.essence = existingEssences.Find(e => e.essenceName == "Water");
                output.costEssenceDecrease = ea;
            }
            if (valueParts[1] == "EoE")
            {
                EssenceAmount ea = new EssenceAmount();
                ea.amount = int.Parse(valueParts[0]);
                ea.essence = existingEssences.Find(e => e.essenceName == "Earth");
                output.costEssenceDecrease = ea;
            }
            if (valueParts[1] == "EoP")
            {
                EssenceAmount ea = new EssenceAmount();
                ea.amount = int.Parse(valueParts[0]);
                ea.essence = existingEssences.Find(e => e.essenceName == "Power");
                output.costEssenceDecrease = ea;
            }
            if (valueParts[1] == "EoM")
            {
                EssenceAmount ea = new EssenceAmount();
                ea.amount = int.Parse(valueParts[0]);
                ea.essence = existingEssences.Find(e => e.essenceName == "Magic");
                output.costEssenceDecrease = ea;
            }
            if (valueParts[1] == "EoF")
            {
                EssenceAmount ea = new EssenceAmount();
                ea.amount = int.Parse(valueParts[0]);
                ea.essence = existingEssences.Find(e => e.essenceName == "Fire");
                output.costEssenceDecrease = ea;
            }
            if (valueParts[1] == "EoI")
            {
                EssenceAmount ea = new EssenceAmount();
                ea.amount = int.Parse(valueParts[0]);
                ea.essence = existingEssences.Find(e => e.essenceName == "Ice");
                output.costEssenceDecrease = ea;
            }
        }
        else if (parts[0] == "Artifacts")
        {
            output.upgradeType = RecipeUpgradeType.decreaseArtifacts;
            value = value.TrimStart(new char[] { ' ', '-' });
            string[] valueParts = value.Split(new char[] { ' ' }, 2);
            InventoryArtifact ia = new InventoryArtifact();
            ia.count = int.Parse(valueParts[0]);
            ia.artifact = existingArtifacts.Find(a => a.name == valueParts[1]);
            output.costArtifactDecrease = ia;
        }
        else if (parts[0] == "Animals")
        {
            output.upgradeType = RecipeUpgradeType.decreaseAnimals;
            value = value.TrimStart(new char[] { ' ', '-' });
            string[] valueParts = value.Split(new char[] { ' ' }, 2);
            InventoryAnimal ia = new InventoryAnimal();
            ia.count = int.Parse(valueParts[0]);
            ia.animal = existingAnimals.Find(a => a.name == valueParts[1]);
            output.costAnimalDecrease = ia;
        }
        else if (parts[0] == "Minimal Rarity")
        {
            output.upgradeType = RecipeUpgradeType.changeRarity;
            string[] valueParts = value.Split(' ');
            if (valueParts[1] == "Good")
            {
                output.newRarity = Rarity.Good;
            }
            if (valueParts[1] == "Rare")
            {
                output.newRarity = Rarity.Rare;
            }
            if (valueParts[1] == "Epic")
            {
                output.newRarity = Rarity.Epic;
            }
            if (valueParts[1] == "Legendary")
            {
                output.newRarity = Rarity.Legendary;
            }
        }
        else if (parts[0] == "Recipe")
        {
            output.upgradeType = RecipeUpgradeType.unlockRecipe;
            value = value.TrimStart(new char[] { ' ', '-' });
            output.unlockedRecipe = existingRecipes.Find(r => r.animal.name == value);
        }
        return output;
    }
}
