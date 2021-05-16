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

    public TutorialText upperText;
    public TutorialText lowerText;

    public void DisableAllExcept(TutorialPanel highlight)
    {
        left.GetComponent<RectTransform>().sizeDelta = new Vector2(highlight.left, 1920);
        right.GetComponent<RectTransform>().sizeDelta = new Vector2(1080 - highlight.left - highlight.width, 1920);
        top.GetComponent<RectTransform>().sizeDelta = new Vector2(highlight.width, highlight.top);
        bottom.GetComponent<RectTransform>().sizeDelta = new Vector2(highlight.width, 1920 - highlight.top - highlight.height);
        top.GetComponent<RectTransform>().anchoredPosition = new Vector3(highlight.left, 0, 0);
        bottom.GetComponent<RectTransform>().anchoredPosition = new Vector3(highlight.left, 0, 0);
        //backgroundBlur.SetActive(true);
    }

    public void EnableAll()
    {
        left.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 1920);
        right.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 1920);
        top.GetComponent<RectTransform>().sizeDelta = new Vector2(1080, 0);
        bottom.GetComponent<RectTransform>().sizeDelta = new Vector2(1080, 0);
        top.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        bottom.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }
}
