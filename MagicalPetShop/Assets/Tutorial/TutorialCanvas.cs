using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCanvas : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    public GameObject top;
    public GameObject bottom;

    public GameObject leftArrow;
    public GameObject rightArrow;

    public TutorialText upperText;
    public TutorialText lowerText;
    public GameObject mask;
    public CutoutMaskUI imageDarkening;

    public void Start()
    {
        upperText.gameObject.GetComponent<Image>().color = UIPalette.THIS.HeaderColor;
        lowerText.gameObject.GetComponent<Image>().color = UIPalette.THIS.HeaderColor;
    }

    public void DisableAll(bool noDarkening = false, bool instant = false)
    {
        left.GetComponent<RectTransform>().sizeDelta = new Vector2(540, 1920);
        right.GetComponent<RectTransform>().sizeDelta = new Vector2(540, 1920);
        top.GetComponent<RectTransform>().sizeDelta = new Vector2(1080, 0);
        bottom.GetComponent<RectTransform>().sizeDelta = new Vector2(1080, 0);
        if (!instant)
        {
            if (!noDarkening)
            {
                mask.GetComponent<RectTransform>().DOSizeDelta(new Vector2(0, 0), 0.5f).SetEase(Ease.OutCubic);
                mask.GetComponent<RectTransform>().DOAnchorPos(new Vector3(540, -960, 0), 0.5f).SetEase(Ease.OutCubic);
                imageDarkening.DOColor(new Color(0, 0, 0, 0.6f), 0.5f).SetEase(Ease.OutCubic);
            }
            else
            {
                mask.GetComponent<RectTransform>().DOSizeDelta(new Vector2(0, 0), 0.5f).SetEase(Ease.OutCubic);
                mask.GetComponent<RectTransform>().DOAnchorPos(new Vector3(540, -960, 0), 0.5f).SetEase(Ease.OutCubic);
                imageDarkening.DOColor(new Color(0, 0, 0, 0.0f), 0.5f).SetEase(Ease.OutCubic);
                Invoke(nameof(DisableMask), 0.01f);
                Invoke(nameof(EnableMask), 0.05f);
            }
        }
        else
        {
            if (!noDarkening)
            {
                mask.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                mask.GetComponent<RectTransform>().anchoredPosition = new Vector3(540, -960, 0);
                imageDarkening.color = new Color(0, 0, 0, 0.6f);
            }
            else
            {
                mask.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                mask.GetComponent<RectTransform>().anchoredPosition = new Vector3(540, -960, 0);
                imageDarkening.color = new Color(0, 0, 0, 0.0f);
                Invoke(nameof(DisableMask), 0.01f);
                Invoke(nameof(EnableMask), 0.05f);
            }
        }
    }

    public void EnableMask()
    {
        mask.GetComponent<Mask>().enabled = true;
    }

    public void DisableMask()
    {
        mask.GetComponent<Mask>().enabled = false;
    }

    public void DisableAllExcept(TutorialPanel highlight, bool firstTime = false, bool debug=false)
    {
        if (debug)
        {
            Debug.Log(highlight.left);
            Debug.Log(highlight.top);
            Debug.Log(highlight.width);
            Debug.Log(highlight.height);
        }
        left.GetComponent<RectTransform>().sizeDelta = new Vector2(highlight.left, 1920);
        right.GetComponent<RectTransform>().sizeDelta = new Vector2(1080 - highlight.left - highlight.width, 1920);
        top.GetComponent<RectTransform>().sizeDelta = new Vector2(highlight.width, highlight.top);
        bottom.GetComponent<RectTransform>().sizeDelta = new Vector2(highlight.width, 1920 - highlight.top - highlight.height);
        top.GetComponent<RectTransform>().anchoredPosition = new Vector3(highlight.left, 0, 0);
        bottom.GetComponent<RectTransform>().anchoredPosition = new Vector3(highlight.left, 0, 0);
        mask.GetComponent<RectTransform>().DOSizeDelta(new Vector2(highlight.width, highlight.height), 0.5f).SetEase(Ease.OutCubic);
        mask.GetComponent<RectTransform>().DOAnchorPos(new Vector3(highlight.left, -highlight.top, 0), 0.5f).SetEase(Ease.OutCubic);
        mask.GetComponent<Mask>().enabled = true;
        if (firstTime)
        {
            Invoke(nameof(DisableMask), 0.01f);
            Invoke(nameof(EnableMask), 0.05f);
        }
        imageDarkening.DOColor(new Color(0, 0, 0, 0.6f), 0.5f).SetEase(Ease.OutCubic);
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
        mask.GetComponent<RectTransform>().DOSizeDelta(new Vector2(1100, 1940), 0.5f).SetEase(Ease.OutCubic);
        mask.GetComponent<RectTransform>().DOAnchorPos(new Vector3(-10, 10, 0), 0.5f).SetEase(Ease.OutCubic);
        imageDarkening.DOColor(new Color(0, 0, 0, 0.0f), 0.5f).SetEase(Ease.OutCubic);
    }
}
