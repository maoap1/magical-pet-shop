using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cauldron : MonoBehaviour
{
    public RecipeSelection recipeSelection;
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            FindObjectOfType<AudioManager>().Play(AudioType.Cauldron);
            recipeSelection.Open();
        }
    }
}
