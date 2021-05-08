using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeSelection : MonoBehaviour
{
    public RecipeInfo recipeInfo;
    public ConfirmationPanel confirmationPanel;

    public List<GameObject> objectsToAppear;
    public List<GameObject> objectsToHide;
    public GameObject mergingToggle;

    public RecipeLocationFilter defaultRecipeCategory;

    private int upgradeCost;

    public void Open()
    {
        this.gameObject.SetActive(true);
        foreach (GameObject g in objectsToAppear)
        {
            g.SetActive(true);
        }
        foreach (GameObject g in objectsToHide)
        {
            g.SetActive(false);
        }
        defaultRecipeCategory.Display();
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
        foreach (GameObject g in objectsToAppear)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in objectsToHide)
        {
            g.SetActive(true);
        }
    }
}
