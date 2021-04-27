using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    [SerializeField]
    List<GameObject> objectsToHide;
    [SerializeField]
    List<GameObject> objectsToAppear;

    public RecipeInfo recipeInfo;

    [SerializeField]
    private GridLayoutGroup animalsGrid;

    [SerializeField]
    private GameObject animalSlot;

    [SerializeField]
    private GridLayoutGroup artifactsGrid;

    [SerializeField]
    private GameObject artifactSlot;

    [SerializeField]
    private EssencesUI essencesUI;

    public void Open() {
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
        // clear everything
        Clear();
        // add items from inventory to the grid layout
        DisplayItems();
    }

    private void DisplayItems() {
        // display animals
        var animals = Inventory.GetOrderedAnimals();
        foreach (InventoryAnimal animal in animals) {
            GameObject newSlot = Instantiate(animalSlot) as GameObject;
            newSlot.GetComponent<AnimalSlot>().SetAnimal(animal, this);
            newSlot.SetActive(true);
            newSlot.transform.SetParent(animalsGrid.transform, false);
        }
        // display artifacts
        var artifacts = Inventory.GetOrderedArtifacts();
        foreach (InventoryArtifact artifact in artifacts) {
            GameObject newSlot = Instantiate(artifactSlot) as GameObject;
            newSlot.GetComponent<ArtifactSlot>().SetArtifact(artifact);
            newSlot.SetActive(true);
            newSlot.transform.SetParent(artifactsGrid.transform, false);
        }
        // display essences
        essencesUI.Refresh();
    }

    private void Clear() {
        // clear animals
        int c = animalsGrid.transform.childCount;
        for (int i = c - 1; i >= 0; i--)
            GameObject.Destroy(animalsGrid.transform.GetChild(i).gameObject);
        // clear artifacts
        c = artifactsGrid.transform.childCount;
        for (int i = c - 1; i >= 0; i--)
            GameObject.Destroy(artifactsGrid.transform.GetChild(i).gameObject);
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
