using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPositionFollower : MonoBehaviour
{
    public RectTransform Target;
    public float TweenSpeed;

    private bool tweening = false;
    private RectTransform rt;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!tweening) return;

        if (Target == null) return;
        if (rt == null) rt = GetComponent<RectTransform>();
        rt.anchoredPosition += Time.deltaTime * (Target.anchoredPosition - rt.anchoredPosition) * TweenSpeed;

    }

    public void TeleportToTarget()
    {
        if (rt == null) rt = GetComponent<RectTransform>();
        rt.anchoredPosition = Target.anchoredPosition;
        tweening = true;
    }
}
