using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackOverviewUI : MonoBehaviour {

    [SerializeField]
    List<GameObject> objectsToHideFromExpedition;
    [SerializeField]
    List<GameObject> objectsToHideFromLeaders;
    [SerializeField]
    List<GameObject> objectsToAppear;
    [SerializeField]
    AnimalsUI animalsUI;

    [SerializeField]
    GridLayoutGroup slotsGrid;
    [SerializeField]
    LocationSlotUI locationSlot;

    Pack pack;
    bool openedFromExpedition = false;

    public void Open(Pack pack) {
        Debug.Log("Pack overview opened");
        this.pack = pack;
        Refresh();
        this.gameObject.SetActive(true);
        foreach (GameObject g in objectsToAppear) {
            g.SetActive(true);
        }
        if (this.openedFromExpedition) {
            foreach (GameObject g in objectsToHideFromExpedition) {
                g.SetActive(false);
            }
        } else {
            foreach (GameObject g in objectsToHideFromLeaders) {
                g.SetActive(false);
            }
        }
    }

    public void Open(Pack pack, bool fromExpedition) {
        this.openedFromExpedition = fromExpedition;
        Open(pack);
    }

    public void Close() {
        this.gameObject.SetActive(false);
        foreach (GameObject g in objectsToAppear) {
            g.SetActive(false);
        }
        if (this.openedFromExpedition) {
            foreach (GameObject g in objectsToHideFromExpedition) {
                g.SetActive(true);
            }
        } else {
            foreach (GameObject g in objectsToHideFromLeaders) {
                g.SetActive(true);
            }
        }
    }

    public void Refresh() {
        // clear everything
        Clear();
        // display current info
        DisplayItems();
    }

    private void DisplayItems() {
        // TODO: display location slots - if the pack is exploring, pass false to the Initialize method, true otherwise
        //      pass also this.animalsUI
        for (int i = 0; i < 3; ++i) {
            LocationSlotUI location = Instantiate(locationSlot, this.slotsGrid.transform).GetComponent<LocationSlotUI>();
            location.Initialize(this.pack, i, new LocationType(), true, this.animalsUI);
        }
        for (int i = 0; i < 2; ++i) {
            LocationSlotUI location = Instantiate(locationSlot, this.slotsGrid.transform).GetComponent<LocationSlotUI>();
            location.Initialize(this.pack, i, new LocationType(), false, this.animalsUI);
        }
    }

    private void Clear() {
        int c = slotsGrid.transform.childCount;
        for (int i = c - 1; i >= 0; i--)
            GameObject.Destroy(slotsGrid.transform.GetChild(i).gameObject);
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
