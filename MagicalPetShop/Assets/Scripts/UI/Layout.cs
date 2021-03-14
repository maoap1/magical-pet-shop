using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Layout : MonoBehaviour
{
    public float GetContainerHeight()
    {
        return gameObject.GetComponent<RectTransform>().rect.height - gameObject.GetComponent<LayoutGroup>().padding.vertical;
    }
    public float GetContainerWidth()
    {
        return gameObject.GetComponent<RectTransform>().rect.width - gameObject.GetComponent<LayoutGroup>().padding.horizontal;
    }
    public void Update()
    {
        Debug.Log(GetContainerHeight());
    }
}
