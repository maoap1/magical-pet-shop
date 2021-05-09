using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatsUI : MonoBehaviour
{

    public void ToggleVisibility() {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    private void OnGUI() {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        int width = 200;
        int height = 100;
        int offsetX = 0;
        int offsetY = 10;
        // Add 10 000 money
        if (GUI.Button(new Rect(offsetX, offsetY, width, height), "Money")) {
            Inventory.AddToInventory(10000);
        }
        // Instantly give maximum level for all the recipes we have
        if (GUI.Button(new Rect(offsetX, offsetY + 1 * height, width, height), "Recipes")) {
            int recipesCount = PlayerState.THIS.recipes.Count;
            for (int i = 0; i < recipesCount; ++i) {
                RecipeProgress recipe = PlayerState.THIS.recipes[i];
                int count = recipe.recipe.recipeLevels[recipe.recipe.recipeLevels.Count - 1].treshold;
                count -= recipe.animalsProduced;
                for (int j = 0; j < count; ++j) recipe.animalProduced();
            }
        }
        // Full essence collectors
        if (GUI.Button(new Rect(offsetX + 1*width, 10, width, height), "Essences")) {
            foreach (EssenceAmount essence in PlayerState.THIS.resources) {
                essence.IncreaseAmount(essence.limit);
            }
        }
        // Finish all expeditions
        if (GUI.Button(new Rect(offsetX + 1 * width, offsetY + 1 * height, width, height), "Expeditions")) {
            foreach (Expedition expedition in PlayerState.THIS.expeditions) {
                expedition.fillRate = 1;
            }
        }
        // Finish all crafting
        if (GUI.Button(new Rect(offsetX + 2 * width, offsetY, width, height), "Crafting")) {
            foreach (CraftedAnimal craftedAnimal in PlayerState.THIS.crafting) {
                if (craftedAnimal.isRecipe) craftedAnimal.fillRate = 1;
            }
        }
        // Finish all merging
        if (GUI.Button(new Rect(offsetX + 2 * width, offsetY + 1 * height, width, height), "Merging")) {
            foreach (CraftedAnimal craftedAnimal in PlayerState.THIS.crafting) {
                if (!craftedAnimal.isRecipe) craftedAnimal.fillRate = 1;
            }
        }
        // Normal speed
        if (GUI.Button(new Rect(offsetX + 3 * width, offsetY, width, height), "x1")) {
            GameLogic.THIS.SetSpeed(1);
        }
        // x10 speed
        if (GUI.Button(new Rect(offsetX + 3 * width, offsetY + 1 * height, width, height), "x10")) {
            GameLogic.THIS.SetSpeed(10);
        }
        // x100 speed
        if (GUI.Button(new Rect(offsetX + 4 * width, offsetY, width, height), "x100")) {
            GameLogic.THIS.SetSpeed(100);
        }
        // x1000 speed
        if (GUI.Button(new Rect(offsetX + 4 * width, offsetY + 1 * height, width, height), "x1000")) {
            GameLogic.THIS.SetSpeed(1000);
        }
#endif
    }
}
