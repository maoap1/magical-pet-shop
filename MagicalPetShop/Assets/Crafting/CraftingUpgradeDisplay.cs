using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Analytics;

public class CraftingUpgradeDisplay : MonoBehaviour
{
    public ResourceCost costDisplay;
    public Button accept;
    private int cost;
    private CraftingInfo craftingInfo;

    private Color inactiveButtonColor;
    private Image imageComponent;
    public void Open(CraftingInfo ci)
    {
        GameLogic.THIS.buyingCraftingSlot = true;
        craftingInfo = ci;
        cost = GameLogic.THIS.craftingSlotUpgrades[PlayerState.THIS.craftingSlots - 1].cost;
        costDisplay.SetCost(cost);
        this.gameObject.SetActive(true);
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(costDisplay.GetComponent<RectTransform>());
        this.inactiveButtonColor = UIPalette.THIS.GetColor(PaletteColor.Inactive);
        this.imageComponent = accept.gameObject.GetComponent<Image>();
        GetComponent<AppearHideComponent>().Do();
    }

    public void Close() {
        this.gameObject.SetActive(false);
        GetComponent<AppearHideComponent>().Revert();
    }

    public void Reject()
    {
        GameLogic.THIS.buyingCraftingSlot = false;
        this.gameObject.SetActive(false);
        Close();
    }

    public void Accept()
    {
        if (canUpgrade()) {
            Inventory.TakeFromInventory(cost);
            PlayerState.THIS.craftingSlots++;
            PlayerState.THIS.Save();
            Analytics.LogEvent("crafting_upgraded", new Parameter("crafting_slots", PlayerState.THIS.craftingSlots));
            GameLogic.THIS.buyingCraftingSlot = false;
            this.gameObject.SetActive(false);
            Close();
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
        if (canUpgrade()) {
            imageComponent.color = Color.white;
            accept.interactable = true;
        }
        else {
            imageComponent.color = this.inactiveButtonColor;
            accept.interactable = false;
        }
    }
}
