using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BlurTween : MonoBehaviour
{

    public float duration;
    public float backDuration;

    public float BlurPower = 0.002f;
    public float LightMultiplier = 0.5f;

    public AnimationCurve tweenType;


    public void Start()
    {
        var mat = GetComponent<Image>().material;
        mat.SetFloat("_BlurPower", 0.0f);
        mat.SetFloat("_LightMultiplier", 1.0f);
    }
    public void Blur()
    {
        var mat = GetComponent<Image>().material;
        mat.DOFloat(BlurPower, "_BlurPower", duration)
            .SetEase(tweenType);
        mat.DOFloat(LightMultiplier, "_LightMultiplier", duration)
            .SetEase(tweenType);
    }

    public void Revert()
    {
        var mat = GetComponent<Image>().material;
        mat.DOFloat(0f, "_BlurPower", backDuration)
            .SetEase(Ease.InCubic);
        mat.DOFloat(1f, "_LightMultiplier", backDuration)
            .SetEase(Ease.InCubic);
    }
}
