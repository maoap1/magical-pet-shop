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
        this.gameObject.SetActive(true);
        GetComponent<AppearHideComponent>().Do();
        defaultRecipeCategory.Display();
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
        GetComponent<AppearHideComponent>().Revert();
    }
}
