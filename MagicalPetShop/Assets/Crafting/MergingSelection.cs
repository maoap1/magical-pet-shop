using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergingSelection : MonoBehaviour
{
    public List<GameObject> objectsToAppear;
    public List<GameObject> objectsToHide;
    public RecipeSelection recipes;

    public MergingLocationFilter defaultMergingCategory;

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
        defaultMergingCategory.Display();
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

    public void ShutDown()
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
        recipes.Close();
    }
}
