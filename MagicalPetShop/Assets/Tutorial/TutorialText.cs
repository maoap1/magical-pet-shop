using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    public void Display(string displayedText)
    {
        text.text = displayedText;
        this.gameObject.TweenAwareEnable();
    }

    public void Close()
    {
        this.gameObject.TweenAwareDisable();
    }
}
