using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AppearHideComponent))]
public class UIPanel : MonoBehaviour
{
    public void Open()
    {
        GetComponent<AppearHideComponent>().Do();
    }

    public void Close()
    {
        GetComponent<AppearHideComponent>().Revert();
    }
}
