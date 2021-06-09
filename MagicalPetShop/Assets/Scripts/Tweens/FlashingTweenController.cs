using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class FlashingTweenController : MonoBehaviour
{
    public float duration;
    public float smallSize;

    private List<Transform> targets = new List<Transform>();
    private Stack<int> toDestroy = new Stack<int>();

    public void SetSize(float value)
    {
        Vector3 newScale = value*Vector3.one;
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null) toDestroy.Push(i);
            else targets[i].localScale = newScale;
        }
        while (toDestroy.Count > 0)
        {
            targets.RemoveAt(toDestroy.Pop());
        }
    }

    public void AddNew(Transform target)
    {
        targets.Add(target);
    }

    public void Start()
    {
        DOTween.To(SetSize, 1f, smallSize, duration).SetLoops(-1, LoopType.Yoyo);
    }
}
