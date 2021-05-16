using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AppearHideComponent))]
public class UIPanel : MonoBehaviour
{
    public void Open()
    {
        this.gameObject.SetActive(true);
        GetComponent<AppearHideComponent>().Do();
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
        GetComponent<AppearHideComponent>().Revert();
    }
}
