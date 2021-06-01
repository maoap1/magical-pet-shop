using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Shows pack slot (possibly with assigned animal) in a pack overview
public class LocationSlotUI : MonoBehaviour
{

    [SerializeField]
    Image animalIcon;
    [SerializeField]
    Image locationIcon;
    [SerializeField]
    GameObject plusSign;
    [SerializeField]
    Material normalMaterial;
    [SerializeField]
    Material graygcaleMaterial;
    [SerializeField]
    Image background;

    AnimalsUI animalsUI;
    Pack pack;
    PackSlot slot;
    int expeditionLevel;
    Color defaultBackgroundColor;
    Color defaultPlusSignColor;

    public void Initialize(Pack pack, PackSlot slot, AnimalsUI animalsUI, int expeditionLevel) {
        this.pack = pack;
        this.slot = slot;
        this.normalMaterial = new Material(this.normalMaterial);
        this.graygcaleMaterial = new Material(this.graygcaleMaterial);
        this.defaultBackgroundColor = UIPalette.THIS.GetColor(background.GetComponent<ImageColor>().color);
        this.defaultPlusSignColor = UIPalette.THIS.GetColor(plusSign.GetComponent<TMPColor>().color);
        if (slot.animal != null) {
            this.animalIcon.gameObject.SetActive(true);
            this.plusSign.SetActive(false);
            this.animalIcon.sprite = slot.animal.animal.artwork;
            if (pack.busy) {
                this.animalIcon.material = this.graygcaleMaterial;
                this.animalIcon.material.SetFloat("_GrayscaleAmount", 1);
            } else {
                this.animalIcon.material = this.normalMaterial;
                this.animalIcon.material.SetColor("_Color", GameGraphics.THIS.getRarityColor(slot.animal.rarity));
                this.animalIcon.material.SetTexture("_BloomTex", slot.animal.animal.bloomSprite.texture);
            }
        } else {
            this.animalIcon.gameObject.SetActive(false);
            this.plusSign.SetActive(true);
        }

        if (pack.busy) {
            this.plusSign.GetComponent<TMPro.TextMeshProUGUI>().color = Color.white;
            this.background.color = UIPalette.THIS.GetColor(PaletteColor.Inactive);
        } else {
            this.plusSign.GetComponent<TMPro.TextMeshProUGUI>().color = this.defaultPlusSignColor;
            this.background.color = this.defaultBackgroundColor;
        }

        this.locationIcon.sprite = slot.location.artwork;
        this.gameObject.GetComponent<Button>().interactable = !pack.busy; // if the pack is exploring, it cannot be changed
        this.animalsUI = animalsUI;
        this.expeditionLevel = expeditionLevel;
    }

    public void Clicked() {
        if (this.slot.animal != null) {
            PacksManager.UnassignAnimal(this.pack, this.slot);
        }
        this.animalsUI.Open(this.pack, this.slot, this.expeditionLevel);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
