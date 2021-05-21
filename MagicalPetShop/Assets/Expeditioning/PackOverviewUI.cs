using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Shows information of a specific pack - slots with traits and assigned animals, total power, ...
public class PackOverviewUI : MonoBehaviour {

    [SerializeField]
    List<GameObject> objectsToHideFromExpedition;
    [SerializeField]
    List<GameObject> objectsToHideFromLeaders;
    [SerializeField]
    AnimalsUI animalsUI;

    [SerializeField]
    TextMeshProUGUI nameText;
    [SerializeField]
    Image iconImage;
    [SerializeField]
    TextMeshProUGUI powerText;
    [SerializeField]
    TextMeshProUGUI statusText;
    [SerializeField]
    GridLayoutGroup slotsGrid;
    [SerializeField]
    LocationSlotUI locationSlot;

    Pack pack;
    bool openedFromExpedition = false;
    int expeditionLevel = 0;

    public void Open(Pack pack) {
        this.openedFromExpedition = false;
        Open_Internal(pack);
    }

    public void Open(Pack pack, bool fromExpedition, int expeditionLevel) {
        this.openedFromExpedition = fromExpedition;
        this.expeditionLevel = expeditionLevel;
        Open_Internal(pack);
    }

    private void Open_Internal(Pack pack) {
        this.pack = pack;
        Refresh();
        this.gameObject.SetActive(true);
        /*
        foreach (GameObject g in objectsToAppear) {
            g.SetActive(true);
        }
        */
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

    public void OnEnable() {
        if (this.pack != null) Refresh();
    }

    public void Close() {
        this.gameObject.SetActive(false);
        /*
        foreach (GameObject g in objectsToAppear) {
            g.SetActive(false);
        }
        */
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
        Clear();
        DisplayData();
    }

    private void DisplayData() {
        this.nameText.text = this.pack.name;
        this.iconImage.sprite = this.pack.artwork;
        this.powerText.text = this.pack.GetTotalPower().ToString();
        this.statusText.text = this.pack.busy ? "EXPLORING" : "FREE";

        // display location slots
        foreach (PackSlot slot in this.pack.slots) {
            LocationSlotUI location = Instantiate(locationSlot, this.slotsGrid.transform).GetComponent<LocationSlotUI>();
            location.Initialize(this.pack, slot, this.animalsUI, this.expeditionLevel);
        }
    }

    private void Clear() {
        int c = slotsGrid.transform.childCount;
        for (int i = c - 1; i >= 0; i--)
            GameObject.Destroy(slotsGrid.transform.GetChild(i).gameObject);
    }

}
