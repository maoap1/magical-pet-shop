using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CostPartRecipe : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI text;
    public int cost
    {
        set { text.text = value.ToString(); }
    }
    public Sprite sprite
    {
        set { icon.sprite = value; }
    }
}
