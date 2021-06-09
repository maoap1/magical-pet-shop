using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashingTween : MonoBehaviour
{
    public float duration;
    public float offset;
    private Tween tween;
    
    public void StartTweening()
    {
        tween = transform.DOScale(offset, duration).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        tween.Kill();
    }

}
