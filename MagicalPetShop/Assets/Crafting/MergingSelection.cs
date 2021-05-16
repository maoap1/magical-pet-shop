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
        foreach (GameObject g in GetComponent<AppearHideComponent>().ObjectsToAppear)
        {
            g.SetActive(true);
        }
        foreach (GameObject g in GetComponent<AppearHideComponent>().ObjectsToHide)
        {
            g.SetActive(false);
        }
        defaultMergingCategory.Display();
    }

    public void Close()
    {
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

    public void ShutDown()
    {
        this.gameObject.SetActive(false);
        foreach (GameObject g in GetComponent<AppearHideComponent>().ObjectsToAppear)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in GetComponent<AppearHideComponent>().ObjectsToHide)
        {
            g.SetActive(true);
        }
        recipes.Close();
    }
}
