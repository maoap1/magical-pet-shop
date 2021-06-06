
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;

public class LightFlashingTween : MonoBehaviour
{
    public float StartingIntensityPercents;
    public float EndingIntensityPercents;
    public float Duration;
    public float MaxOffset;

    private Light2D light;
    private float startingIntensity;

    private void SetIntensity(float value)
    {
        light.intensity = value;
    }

    public void Start()
    {
        light = GetComponent<Light2D>();
        startingIntensity = light.intensity;
        var tween = DOTween.To(SetIntensity,
                   startingIntensity * StartingIntensityPercents / 100,
                   startingIntensity * EndingIntensityPercents / 100,
                   Duration)
            .SetLoops(-1, LoopType.Yoyo);
        tween.fullPosition = Random.Range(0f, MaxOffset);
            
    }



}
