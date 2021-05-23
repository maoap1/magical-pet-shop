using UnityEngine;
using DG.Tweening;

public class SlidingTween : MonoBehaviour
{
    public float targetY;
    public float startY;
    public float duration;
    public float backDuration;
    public bool disable = true;
    public bool enable = true;

    public AnimationCurve tweenType;

    private void SetY(float target)
    {
        var rt = GetComponent<RectTransform>();
        rt.anchoredPosition = rt.anchoredPosition.WithY(target);
    }

    public void SlideY()
    {
        SetY(startY);
        if (enable) gameObject.SetActive(true);
        GetComponent<RectTransform>().DOAnchorPosY(targetY, duration)
            .SetEase(tweenType);
    }

    public void SlideYBack()
    {
        GetComponent<RectTransform>().DOAnchorPosY(startY, backDuration)
            .SetEase(Ease.InCubic)
            .OnComplete(() => { if (disable) gameObject.SetActive(false); });
    }
}
