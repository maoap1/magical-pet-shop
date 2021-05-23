using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AppearHideComponent))]
public class RecipeSelection : MonoBehaviour
{
    public RecipeInfo recipeInfo;
    public ConfirmationPanel confirmationPanel;

    public GameObject mergingToggle;

    public RecipeLocationFilter defaultRecipeCategory;

    private int upgradeCost;

    public void Open()
    {
        GameLogic.THIS.inCrafting = true;
        this.gameObject.SetActive(true);
        foreach (GameObject g in GetComponent<AppearHideComponent>().ObjectsToAppear)
        {
            g.SetActive(true);
        }
        foreach (GameObject g in GetComponent<AppearHideComponent>().ObjectsToHide)
        {
            g.SetActive(false);
        }
        defaultRecipeCategory.Display();
    }

    public void Close()
    {
        GameLogic.THIS.inCrafting = false;
        this.gameObject.SetActive(false);
        foreach (GameObject g in GetComponent<AppearHideComponent>().ObjectsToAppear)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in GetComponent<AppearHideComponent>().ObjectsToHide)
        {
            g.SetActive(true);
        }
    }
}
