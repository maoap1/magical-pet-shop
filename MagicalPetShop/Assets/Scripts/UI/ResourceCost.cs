using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceCost : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI costText;
    public void SetCost(Sprite icon, int cost)
    {
        this.icon.sprite = icon;
        costText.text = cost.ToString();
    }
}
