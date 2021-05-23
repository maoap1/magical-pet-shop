using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BlurTween : MonoBehaviour
{

    public float duration;
    public float backDuration;


    public void Blur()
    {
        var mat = GetComponent<Image>().material;
        mat.SetFloat("_BlurPower", 0.0f);
        mat.SetFloat("_LightMultiplier", 1.0f);

        mat.DOFloat(0.002f, "_BlurPower", duration);
        mat.DOFloat(0.5f, "_LightMultiplier", duration);
    }

    public void Revert()
    {
        var mat = GetComponent<Image>().material;
        mat.DOFloat(0f, "_BlurPower", backDuration);
        mat.DOFloat(1f, "_LightMultiplier", backDuration);
    }
}
