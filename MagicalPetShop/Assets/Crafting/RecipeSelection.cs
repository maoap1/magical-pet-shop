using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AppearHideComponent))]
public class RecipeSelection : MonoBehaviour
{
    public RecipeInfo recipeInfo;
    public MergingButton mergingButton;
    public ConfirmationPanel confirmationPanel;

    public GameObject mergingToggle;

    public RecipeLocationFilter defaultRecipeCategory;

    private int upgradeCost;

    public void Open()
    {
        GameLogic.THIS.inCrafting = true;
        this.mergingButton.Reset();
        GetComponent<AppearHideComponent>().Do();
        defaultRecipeCategory.Display();
    }

    public void Close()
    {
        GameLogic.THIS.inCrafting = false;
        GetComponent<AppearHideComponent>().Revert();
    }
}
