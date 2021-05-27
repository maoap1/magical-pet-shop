using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cauldron : MonoBehaviour
{
    public RecipeSelection recipeSelection;
    private void OnMouseDown()
    {
        if (!Utils.IsPointerOverGameObject())
        {
            FindObjectOfType<AudioManager>().Play(SoundType.Cauldron);
            recipeSelection.Open();
        }
    }

    public void OpenRecipes()
    {
        recipeSelection.Open();
    }
}
