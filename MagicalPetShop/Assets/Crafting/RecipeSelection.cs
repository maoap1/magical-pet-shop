using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeSelection : MonoBehaviour
{
    public GameObject recipesPanel;

    public List<GameObject> objectsToAppear;
    public List<GameObject> objectsToHide;

    private int upgradeCost;

    public void Open()
    {
        Debug.Log("clicked");
        this.gameObject.SetActive(true);
        foreach (GameObject g in objectsToAppear)
        {
            g.SetActive(true);
        }
        foreach (GameObject g in objectsToHide)
        {
            g.SetActive(false);
        }
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
