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
        Rarity rarity = Inventory.HighestRarityToPay(rp.costAnimals);
        highestQuality.text = rarity.ToString("G");
        highestQuality.color = GameGraphics.THIS.getRarityColor(rarity);
        this.gameObject.SetActive(true);
    }

    public void Reject()
    {
        this.gameObject.SetActive(false);
    }

    public void Accept()
    {
        if (Crafting.StartCrafting(recipe))
            FindObjectOfType<AudioManager>().Play(SoundType.Splash);
        this.gameObject.SetActive(false);
    }
}
