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
    int slotIndex;
    LocationType location;

    public void Open(Pack pack, int slotIndex, LocationType location) {
        this.pack = pack;
        this.slotIndex = slotIndex;
        this.location = location;
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
        // display current info
        DisplayItems();
    }

    private void DisplayItems() {
        // TODO: Filter animals according to their locations and display them
        for (int i = 0; i < 3; ++i) {
            LocationAnimalSlotUI slot = Instantiate(locationAnimalSlot, this.animalsGrid.transform).GetComponent<LocationAnimalSlotUI>();
            slot.Initialize(this, this.packOverview, new Animal(), this.pack, this.slotIndex);
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
