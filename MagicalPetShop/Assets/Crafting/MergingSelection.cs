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
        this.gameObject.SetActive(true);
        GetComponent<AppearHideComponent>().Do();
        defaultMergingCategory.Display();
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
        GetComponent<AppearHideComponent>().Revert();
    }

    public void ShutDown()
    {
        this.gameObject.SetActive(false);
        GetComponent<AppearHideComponent>().Revert();
        recipes.Close();
    }
}
