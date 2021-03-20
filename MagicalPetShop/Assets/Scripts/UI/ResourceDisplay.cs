using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI text;
    public Essence essence;
    private EssenceAmount essenceAmount = null;
    // Start is called before the first frame update
    void Start()
    {
        icon.sprite = essence.icon;
    }

    // Update is called once per frame
    void Update()
    {
        if (essenceAmount == null && PlayerState.THIS.resources.Count > 0)
        {
            foreach (EssenceAmount r in PlayerState.THIS.resources)
            {
                if (r.essence.essenceName == essence.essenceName)
                {
                    essenceAmount = r;
                }
            }
        }
        else if (essenceAmount != null)
        {
            text.text = essenceAmount.amount.ToString();
            if (essenceAmount.full)
            {
                text.color = Color.red;
            }
            else
            {
                text.color = Color.white;
            }
        }
    }
}
