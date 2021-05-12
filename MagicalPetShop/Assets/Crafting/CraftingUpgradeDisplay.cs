using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUpgradeDisplay : MonoBehaviour
{
    public ResourceCost costDisplay;
    public Button accept;
    private int cost;
    private CraftingInfo craftingInfo;
    public void Open(CraftingInfo ci)
    {
        craftingInfo = ci;
        cost = GameLogic.THIS.craftingSlotUpgrades[PlayerState.THIS.craftingSlots - 1].cost;
        costDisplay.SetCost(cost);
        this.gameObject.SetActive(true);
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(costDisplay.GetComponent<RectTransform>());
    }

    public void Reject()
    {
        this.gameObject.SetActive(false);
    }

    public void Accept()
    {
        if (canUpgrade()) {
            PlayerState.THIS.money -= cost;
            PlayerState.THIS.craftingSlots++;
            PlayerState.THIS.Save();
            this.gameObject.SetActive(false);
        }
    }

    public bool canUpgrade()
    {
        if (cost <= PlayerState.THIS.money)
        {
            return true;
        }
        return false;
    }

    public void Update()
    {
        if (canUpgrade())
        {
            accept.interactable = true;
        }
        else
        {
            accept.interactable = false;
        }
    }
}
