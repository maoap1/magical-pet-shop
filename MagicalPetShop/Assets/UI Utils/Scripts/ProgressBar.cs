using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float fillRate
    {
        get { return mask.fillAmount; }
        set { mask.fillAmount = Mathf.Clamp(value, 0, 1); }
    }

    public Image mask;
}
