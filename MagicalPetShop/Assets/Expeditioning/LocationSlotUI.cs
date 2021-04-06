using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    int slotIndex;
    Animal animal;
    LocationType location;

    public void Initialize(Pack pack, int slotIndex, LocationType location, bool active, AnimalsUI animalsUI) {
        this.pack = pack;
        this.slotIndex = slotIndex;
        this.location = location;
        this.locationIcon.sprite = location.artwork;
        this.gameObject.GetComponent<Button>().interactable = active;
        this.animalsUI = animalsUI;
    }

    public void SetAnimal(Animal animal) {
        this.animal = animal;
        this.animalIcon.sprite = animal.artwork;
    }

    public void Clicked() {
        if (this.animal != null) {
            PacksManager.UnassignAnimal(this.pack, this.slotIndex);
        }
        this.animalsUI.Open(this.pack, this.slotIndex, this.location);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.animalIcon.sprite = this.emptySlot;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
