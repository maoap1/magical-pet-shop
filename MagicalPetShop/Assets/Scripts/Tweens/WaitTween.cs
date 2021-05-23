using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitTween : MonoBehaviour
{
    public float backDuration;
    public bool disable = true;
    public bool enable = true;

    public void WaitingEnable()
    {
        if (enable) gameObject.SetActive(true);
    }

    public void WaitingDisable()
    {
        if (disable) Invoke(nameof(DisableNow), backDuration);
    }

    private void DisableNow()
    {
        gameObject.SetActive(false);
    }
}
