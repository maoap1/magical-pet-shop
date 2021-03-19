using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    [SerializeField]
    private GridLayoutGroup animalsGrid;

    [SerializeField]
    private GameObject animalSlot;

    public void Refresh() {
        // clear everything
        Clear();
        // add items from inventory to the grid layout
        DisplayItems();
    }

    private void DisplayItems() {
        var animals = Inventory.GetOrderedAnimals();
        foreach (InventoryAnimal animal in animals) {
            GameObject newSlot = Instantiate(animalSlot) as GameObject;
            newSlot.GetComponent<AnimalSlot>().SetAnimal(animal);
            newSlot.SetActive(true);
            newSlot.transform.SetParent(animalsGrid.transform, false);
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
