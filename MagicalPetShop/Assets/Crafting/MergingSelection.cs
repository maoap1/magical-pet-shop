using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AppearHideComponent))]
public class MergingSelection : MonoBehaviour
{
    public RecipeSelection recipes;

    public MergingLocationFilter defaultMergingCategory;

    private int upgradeCost;

    public void Open()
    {
        GetComponent<AppearHideComponent>().Do();
        defaultMergingCategory.Display();
    }

    public void Close()
    {
        GetComponent<AppearHideComponent>().Revert();
    }

    public void ShutDown()
    {
        GetComponent<AppearHideComponent>().Revert();
        recipes.Close();
    }
}
