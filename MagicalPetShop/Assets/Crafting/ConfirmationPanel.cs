using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConfirmationPanel : MonoBehaviour
{
    [HideInInspector]
    public RecipeProgress recipe;
    public TextMeshProUGUI highestQuality;
    public void Open(RecipeProgress rp)
    {
        recipe = rp;
        highestQuality.text = Inventory.HighestRarityToPay(rp.costAnimals).ToString("G");
        this.gameObject.SetActive(true);
    }

    public void Reject()
    {
        this.gameObject.SetActive(false);
    }

    public void Accept()
    {
        Crafting.StartCrafting(recipe);
        this.gameObject.SetActive(false);
    }
}
