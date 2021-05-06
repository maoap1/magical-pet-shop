using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fade : MonoBehaviour
{
    public GameObject blackRect;

    public float x = 0f;

    private void setBlur(float value)
    {
        x = value;
    }

    public void Do()
    {
        var image = blackRect.GetComponent<Image>();
        /*
        Tween t1 = DOTween.To(() => x, setBlur, 3f, 1f);

        Tween t2 = blackRect.GetComponent<Image>().DOFade(1f, 1f).SetEase(Ease.InOutSine).OnComplete(
            () =>
            {
                blackRect.GetComponent<Image>().DOColor(Color.white, 1f).SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    blackRect.GetComponent<Image>().DOFade(0f, 1f).SetEase(Ease.InOutSine);
                });
            }).SetDelay(2f);
        */
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveX(10, 1f));
        sequence.Join(transform.DOScaleX(0.1f, 1f));
        sequence.Join(image.DOColor(Color.red, 1f));
        sequence.AppendInterval(1f);
        sequence.Append(transform.DOMoveY(100, 1f));
        sequence.Join(transform.DOScaleY(10f, 1f));
        sequence.Join(image.DOColor(Color.green, 1f));
        sequence.AppendInterval(1f);

        sequence.SetLoops(-1, LoopType.Yoyo);

                
    }
}
