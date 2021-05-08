#if UNITY_EDITOR
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

    private static List<T> GetExisting<T>() where T : class
    {
        string[] existingPaths = AssetDatabase.FindAssets(String.Format("t:{0}", typeof(T)));
        List<T> existingList = new List<T>();
        foreach (string path in existingPaths)
        {
            existingList.Add(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(path), typeof(T)) as T);
        }
        return existingList;
    }

    private static bool NontrivialName(string name)
        => name != "" && name != "Name";

    private static bool ArtifactExists(string name, List<Artifact> existingArtifacts, GstuSpreadSheet sheet)
    {
        return existingArtifacts.Find(a => a.name == sheet[name, "Artifact"].value) != null;
    }

    private static bool AnimalExists(string name, List<Animal> existingAnimals)
    {
        return existingAnimals.Find(a => a.name == name) != null;
    }

    private static bool RecipeExists(string name, List<Recipe> existingRecipes)
    {
        return existingRecipes.Find(r => r.animal.name == name) != null;
    }


    private static void ReadAndProcessRecipes(GstuSpreadSheet sheet)
    {

        List<Animal> existingAnimals = GetExisting<Animal>();
        List<Artifact> existingArtifacts = GetExisting<Artifact>();
        List<Essence> existingEssences = GetExisting<Essence>();
        List<Recipe> existingRecipes = GetExisting<Recipe>();
        List<LocationType> existingLocations = GetExisting<LocationType>();

        foreach (var nameValue in sheet.columns["Name"])
        {
            string name = nameValue.value;
            //Load artifacts
            if (NontrivialName(name) && !ArtifactExists(name, existingArtifacts, sheet))
            {
                if (sheet[name, "Artifact"].value != "---")
                {
                    Artifact so = ScriptableObject.CreateInstance<Artifact>();
                    so.name = sheet[name, "Artifact"].value;
                    AssetDatabase.CreateAsset(so, "Assets/Artifacts/" + so.name.Replace("/", "").Replace("\\", "") + ".asset");
                    existingArtifacts.Add(so);
                }
            }
            if (NontrivialName(name) && existingArtifacts.Find(a => a.name == sheet[nameValue.value, "Merging artifact"].value) == null)
            {
                Artifact so = ScriptableObject.CreateInstance<Artifact>();
                so.name = sheet[nameValue.value, "Merging artifact"].value;
                AssetDatabase.CreateAsset(so, "Assets/Artifacts/" + so.name.Replace("/", "").Replace("\\", "") + ".asset");
                existingArtifacts.Add(so);
            }
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
            string name = nameValue.value;
            //Load animals
            if (NontrivialName(name) && !AnimalExists(name, existingAnimals))
            {
                if (sheet[name, "Name"].value != "---")
                {
                    Animal so = ScriptableObject.CreateInstance<Animal>();
                    so.name = name;
                    so.level = int.Parse(sheet[name, "Tier"].value);
                    so.value = int.Parse(sheet[name, "Value"].value.Replace(",", ""));
                    so.animalEssence = existingEssences.Find(e => e.name == sheet[name, "Category"].value);
                    so.basePower = int.Parse(sheet[name, "Power"].value.Replace(",", ""));
                    so.associatedArtifact = existingArtifacts.Find(a => a.name == sheet[name, "Merging artifact"].value);
                    so.secondaryCategories = new List<LocationType>();
                    if (sheet[name, "Secondary category"].value != "---")
                    {
                        so.secondaryCategories.Add(existingLocations.Find(l => l.name == sheet[name, "Secondary category"].value));
                    }
                    AssetDatabase.CreateAsset(so, "Assets/Animals/" + so.name.Replace("/", "").Replace("\\", "") + ".asset");
                    existingAnimals.Add(so);
                }
            }
            else if (AnimalExists(name, existingAnimals))
            {
                Animal animal = existingAnimals.Find(a => a.name == name);
                animal.level = int.Parse(sheet[name, "Tier"].value);
                animal.value = int.Parse(sheet[name, "Value"].value.Replace(",", ""));
                animal.animalEssence = existingEssences.Find(e => e.name == sheet[name, "Category"].value);
                animal.basePower = int.Parse(sheet[name, "Power"].value.Replace(",", ""));
                animal.associatedArtifact = existingArtifacts.Find(a => a.name != sheet[name, "Merging artifact"].value);
                EditorUtility.SetDirty(animal);
            }
        }
        foreach (var nameValue in sheet.columns["Name"])
        {
            string name = nameValue.value;
            
            //Load recipes
            if (NontrivialName(name) && !RecipeExists(name, existingRecipes))
            {
                Recipe so = ScriptableObject.CreateInstance<Recipe>();
                so.animal = existingAnimals.Find(a => a.name == name);
                AssetDatabase.CreateAsset(so, "Assets/Recipes/" + so.animal.name.Replace("/", "").Replace("\\", "") + ".asset");
                existingRecipes.Add(so);
            }
        }
        foreach (var nameValue in sheet.columns["Name"])
        {
            string name = nameValue.value;
            if (RecipeExists(name, existingRecipes))
            {
                Recipe recipe = existingRecipes.Find(r => r.animal.name == name);
                recipe.animal = existingAnimals.Find(a => a.name == name);

                recipe.baseCostArtifacts = new List<InventoryArtifact>();


                if (sheet[name, "Artifact"].value != "---")
                {
                    InventoryArtifact iar = new InventoryArtifact();
                    iar.count = int.Parse(sheet[name, "Artifact Amount"].value);
                    iar.artifact = existingArtifacts.Find(a => a.name == sheet[name, "Artifact"].value);
                    recipe.baseCostArtifacts.Add(iar);
                }

                recipe.baseCostAnimals = new List<InventoryAnimal>();
                if (sheet[name, "Animal"].value != "---")
                {
                    InventoryAnimal ian = new InventoryAnimal();
                    ian.count = int.Parse(sheet[name, "Animal Amount"].value);
                    ian.animal = existingAnimals.Find(a => a.name == sheet[name, "Animal"].value);
                    recipe.baseCostAnimals.Add(ian);
                }


                //Essences
                recipe.baseCostEssences = new List<EssenceAmount>();
                if (sheet[name, "Water Essence"].value != "---")
                {
                    EssenceAmount ea = new EssenceAmount();
                    ea.amount = int.Parse(sheet[name, "Water Essence"].value);
                    ea.essence = existingEssences.Find(e => e.essenceName == "Water");
                    recipe.baseCostEssences.Add(ea);
                }
                if (sheet[name, "Earth Essence"].value != "---")
                {
                    EssenceAmount ea = new EssenceAmount();
                    ea.amount = int.Parse(sheet[name, "Earth Essence"].value);
                    ea.essence = existingEssences.Find(e => e.essenceName == "Earth");
                    recipe.baseCostEssences.Add(ea);
                }
                if (sheet[name, "Fire Essence"].value != "---")
                {
                    EssenceAmount ea = new EssenceAmount();
                    ea.amount = int.Parse(sheet[name, "Fire Essence"].value);
                    ea.essence = existingEssences.Find(e => e.essenceName == "Fire");
                    recipe.baseCostEssences.Add(ea);
                }
                if (sheet[name, "Power Essence"].value != "---")
                {
                    EssenceAmount ea = new EssenceAmount();
                    ea.amount = int.Parse(sheet[name, "Power Essence"].value);
                    ea.essence = existingEssences.Find(e => e.essenceName == "Power");
                    recipe.baseCostEssences.Add(ea);
                }
                if (sheet[name, "Magic Essence"].value != "---")
                {
                    EssenceAmount ea = new EssenceAmount();
                    ea.amount = int.Parse(sheet[name, "Magic Essence"].value);
                    ea.essence = existingEssences.Find(e => e.essenceName == "Magic");
                    recipe.baseCostEssences.Add(ea);
                }
                if (sheet[name, "Ice Essence"].value != "---")
                {
                    EssenceAmount ea = new EssenceAmount();
                    ea.amount = int.Parse(sheet[name, "Ice Essence"].value);
                    ea.essence = existingEssences.Find(e => e.essenceName == "Ice");
                    recipe.baseCostEssences.Add(ea);
                }

                recipe.baseDuration = int.Parse(sheet[name, "Crafting Time (seconds)"].value.Replace(",", ""));
                recipe.baseRarity = Rarity.Common;
                //Recipe levels
                recipe.recipeLevels = new List<RecipeLevel>();
                recipe.recipeLevels.Add(parseRecipeLevel(sheet[name, "Crafting Upgrade 1"].value, int.Parse(sheet[name, "Crafts Needed 1"].value), existingEssences, existingArtifacts, existingAnimals, existingRecipes));
                recipe.recipeLevels.Add(parseRecipeLevel(sheet[name, "Crafting Upgrade 2"].value, int.Parse(sheet[name, "Crafts Needed 2"].value), existingEssences, existingArtifacts, existingAnimals, existingRecipes));
                recipe.recipeLevels.Add(parseRecipeLevel(sheet[name, "Crafting Upgrade 3"].value, int.Parse(sheet[name, "Crafts Needed 3"].value), existingEssences, existingArtifacts, existingAnimals, existingRecipes));
                recipe.recipeLevels.Add(parseRecipeLevel(sheet[name, "Crafting Upgrade 4"].value, int.Parse(sheet[name, "Crafts Needed 4"].value), existingEssences, existingArtifacts, existingAnimals, existingRecipes));
                recipe.recipeLevels.Add(parseRecipeLevel(sheet[name, "Crafting Upgrade 5"].value, int.Parse(sheet[name, "Crafts Needed 5"].value), existingEssences, existingArtifacts, existingAnimals, existingRecipes));
                EditorUtility.SetDirty(recipe);
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        //UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(this.gameObject.scene);

        Debug.Log("Recipes loaded");
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
#endif