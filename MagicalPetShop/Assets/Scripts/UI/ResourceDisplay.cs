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
    private bool unlockedImage = false;
    private Button button;

    private Color defaultTextColor = Color.clear;

    // Start is called before the first frame update
    void Start()
    {
        icon.sprite = GameGraphics.THIS.unknown;
        button = gameObject.GetComponent<Button>();
        button.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (defaultTextColor == Color.clear) InitializeColor();
        if (essenceAmount == null && PlayerState.THIS.resources != null && PlayerState.THIS.resources.Count > 0)
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
            if (essenceAmount.unlocked & !unlockedImage)
            {
                button.interactable = true;
                icon.sprite = essenceAmount.essence.icon;
                unlockedImage = true;
            }
            if (essenceAmount.unlocked)
            {
                text.text = essenceAmount.amount.ToString();
                if (essenceAmount.full)
                {
                    text.color = new Color(0, 168, 0);
                }
                else
                {
                    text.color = this.defaultTextColor;
                }
            }
            else
            {
                text.text = "0";
                text.color = this.defaultTextColor;
            }
        }
    }

    private void InitializeColor() {
        TMPColor colorComponent = text.gameObject.GetComponent<TMPColor>();
        if (colorComponent != null)
            this.defaultTextColor = UIPalette.THIS.GetColor(colorComponent.color);
        else {
            this.defaultTextColor = Color.white;
        }
    }
}
