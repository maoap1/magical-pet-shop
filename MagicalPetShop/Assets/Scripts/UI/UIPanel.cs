using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AppearHideComponent))]
public class UIPanel : MonoBehaviour
{
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
}
