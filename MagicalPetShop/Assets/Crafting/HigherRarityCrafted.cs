using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AppearHideComponent))]
public class HigherRarityCrafted : MonoBehaviour
{
    public Image animalImage;
    public TMPro.TextMeshProUGUI animalName;
    public TMPro.TextMeshProUGUI description;
    private RecipeProgress rp;

    public void Open(RecipeProgress rp, Rarity rarity)
    {
        this.rp = rp;
        animalImage.sprite = rp.animal.artwork;
        animalImage.material.SetColor("_Color", GameGraphics.THIS.getRarityColor(rarity));
        animalImage.material.SetTexture("_BloomTex", rp.animal.bloomSprite.texture);
        animalName.text = rp.animal.name;
        description.text = rarity.ToString("G") + " animal crafted!";
        GetComponent<AppearHideComponent>().Do();
        FindObjectOfType<AudioManager>().Play(SoundType.HigherRarity);
    }

    public void Close()
    {
        rp.animalProduced();
        GetComponent<AppearHideComponent>().Revert();
    }
}
