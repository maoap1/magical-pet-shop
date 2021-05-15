using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCanvas : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    public GameObject top;
    public GameObject bottom;

    public GameObject backgroundBlur;

    public void DisableAllExcept(TutorialPanel highlight)
    {
        left.GetComponent<RectTransform>().sizeDelta = new Vector2(highlight.left, 1920);
        right.GetComponent<RectTransform>().sizeDelta = new Vector2(1080 - highlight.left - highlight.width, 1920);
        top.GetComponent<RectTransform>().sizeDelta = new Vector2(1080, highlight.top);
        bottom.GetComponent<RectTransform>().sizeDelta = new Vector2(1080, 1920 - highlight.top - highlight.height);
        backgroundBlur.SetActive(true);
    }
}
