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
    Sprite emptySlot;

    AnimalsUI animalsUI;
    Pack pack;
    PackSlot slot;
    int expeditionLevel;

    public void Initialize(Pack pack, PackSlot slot, AnimalsUI animalsUI, int expeditionLevel) {
        this.pack = pack;
        this.slot = slot;
        if (slot.animal != null) this.animalIcon.sprite = slot.animal.animal.artwork;
        else this.animalIcon.sprite = this.emptySlot;
        this.animalIcon.material = new Material(this.animalIcon.material);
        if (pack.busy) this.animalIcon.material.SetFloat("_GrayscaleAmount", 1);
        else this.animalIcon.material.SetFloat("_GrayscaleAmount", 0);
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