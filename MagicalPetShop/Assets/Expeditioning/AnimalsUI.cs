using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalsUI : MonoBehaviour {

    [SerializeField]
    List<GameObject> objectsToHide;
    [SerializeField]
    List<GameObject> objectsToAppear;
    [SerializeField]
    PackOverviewUI packOverview;

    [SerializeField]
    GridLayoutGroup animalsGrid;
    [SerializeField]
    LocationAnimalSlotUI locationAnimalSlot;

    Pack pack;
    PackSlot slot;
    int expeditionLevel;

    public void Open(Pack pack, PackSlot slot, int expeditionLevel) {
        this.pack = pack;
        this.slot = slot;
        this.expeditionLevel = expeditionLevel;
        Refresh();
        this.gameObject.SetActive(true);
        foreach (GameObject g in objectsToAppear) {
            g.SetActive(true);
        }
        foreach (GameObject g in objectsToHide) {
            g.SetActive(false);
        }
    }

    public void Close() {
        this.gameObject.SetActive(false);
        foreach (GameObject g in objectsToAppear) {
            g.SetActive(false);
        }
        foreach (GameObject g in objectsToHide) {
            g.SetActive(true);
        }
    }

    public void Refresh() {
        Clear();
        DisplayItems();
    }

    private void DisplayItems() {
        // Filter animals according to their locations and display them
        List<InventoryAnimal> animals = Inventory.GetOrderedAnimals();
        List<InventoryAnimal> filteredAnimals = new List<InventoryAnimal>();
        foreach (InventoryAnimal animal in animals) {
            bool hasRequiredLocation = false;
            if (animal.animal.category == this.slot.location) {
                hasRequiredLocation = true;
            } else {
                foreach (LocationType location in animal.animal.secondaryCategories) {
                    if (location == this.slot.location) {
                        hasRequiredLocation = true;
                        break;
                    }
                }
            }
            if (hasRequiredLocation) {
                filteredAnimals.Add(animal);
            }
        }
        // Sort animals - according to their power, then rarity, then tier, finally essence
        filteredAnimals.Sort((a1, a2) => {
            // according to power
            if (a1.GetPower() > a2.GetPower()) return -1;
            if (a2.GetPower() > a1.GetPower()) return 1;
            // according to rarity (higher rarity in top)
            if (a1.rarity > a2.rarity) return -1;
            if (a2.rarity > a1.rarity) return 1;
            // according to level
            if (a1.animal.level > a2.animal.level) return -1;
            if (a2.animal.level > a1.animal.level) return 1;
            // according to essences
            return a1.animal.category.name.CompareTo(a2.animal.category.name);
        });
        // Display sorted animals
        foreach (InventoryAnimal animal in filteredAnimals) {
            LocationAnimalSlotUI slot = Instantiate(locationAnimalSlot, this.animalsGrid.transform).GetComponent<LocationAnimalSlotUI>();
            slot.Initialize(this, this.packOverview, animal, this.pack, this.slot);
        }
    }

    private void Clear() {
        int c = animalsGrid.transform.childCount;
        for (int i = c - 1; i >= 0; i--)
            GameObject.Destroy(animalsGrid.transform.GetChild(i).gameObject);
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
