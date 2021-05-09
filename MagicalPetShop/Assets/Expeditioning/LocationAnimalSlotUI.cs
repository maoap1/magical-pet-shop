using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Used in AnimalsUI to select an animal and assign it to a pecific slot in a pack
public class LocationAnimalSlotUI : MonoBehaviour
{
    [SerializeField]
    Image iconImage;
    [SerializeField]
    Text countText;
    [SerializeField]
    Text nameText;
    [SerializeField]
    Text powerText;
    [SerializeField]
    Text deathProbText;
    [SerializeField]
    GameObject locationSlot;
    [SerializeField]
    HorizontalLayoutGroup locationsLayout;

    AnimalsUI animalsUI;
    PackOverviewUI packOverviewUI;
    InventoryAnimal animal;
    Pack pack;
    PackSlot slot;

    public void Initialize(AnimalsUI animalsUI, PackOverviewUI packOverviewUI, InventoryAnimal animal, Pack pack, PackSlot slot) {
        this.animalsUI = animalsUI;
        this.packOverviewUI = packOverviewUI;
        this.animal = animal;
        this.pack = pack;
        this.slot = slot;

        this.iconImage.sprite = animal.animal.artwork;
        this.iconImage.material = new Material(this.iconImage.material);
        this.iconImage.material.SetColor("_Color", GameGraphics.THIS.getRarityColor(animal.rarity));
        this.iconImage.material.SetTexture("_BloomTex", animal.animal.bloomSprite.texture);
        this.countText.text = animal.count.ToString();
        this.nameText.text = animal.animal.name;
        this.powerText.text = animal.GetPower().ToString();
        this.deathProbText.text = ((int)(animal.GetProbabilityOfDeath() * 100)).ToString();

        // Fill locations layout (first clear)
        int c = this.locationsLayout.transform.childCount;
        for (int i = c - 1; i >= 0; i--)
            GameObject.Destroy(this.locationsLayout.transform.GetChild(i).gameObject);
        LocationIconUI newSlot = Instantiate(this.locationSlot, this.locationsLayout.transform).GetComponent<LocationIconUI>();
        newSlot.Initialize(animal.animal.category);
        foreach (LocationType location in animal.animal.secondaryCategories) {
            newSlot = Instantiate(this.locationSlot, this.locationsLayout.transform).GetComponent<LocationIconUI>();
            newSlot.Initialize(location);
        }
    }

    public void Clicked() {
        PacksManager.AssignAnimal(this.animal, this.pack, this.slot);
        this.animalsUI.Close();
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
