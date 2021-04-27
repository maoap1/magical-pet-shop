using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    Animal animal;
    Pack pack;
    PackSlot slot;

    public void Initialize(AnimalsUI animalsUI, PackOverviewUI packOverviewUI, InventoryAnimal animal, Pack pack, PackSlot slot) {
        this.animalsUI = animalsUI;
        this.packOverviewUI = packOverviewUI;
        this.animal = animal.animal;
        this.pack = pack;
        this.slot = slot;

        this.iconImage.sprite = this.animal.artwork;
        this.countText.text = animal.count.ToString();
        this.nameText.text = this.animal.name;
        this.powerText.text = animal.GetPower().ToString();
        this.deathProbText.text = animal.GetProbabilityOfDeath().ToString();

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
        this.packOverviewUI.Open(this.pack);
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
