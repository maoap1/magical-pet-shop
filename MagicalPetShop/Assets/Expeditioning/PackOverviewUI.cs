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
    Text nameText;
    [SerializeField]
    Image iconImage;
    [SerializeField]
    Text powerText;
    [SerializeField]
    Text statusText;
    [SerializeField]
    GridLayoutGroup slotsGrid;
    [SerializeField]
    LocationSlotUI locationSlot;

    Pack pack;
    bool openedFromExpedition = false;
    int expeditionLevel = 0;

    public void Open(Pack pack) {
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

    public void Open(Pack pack, bool fromExpedition, int expeditionLevel) {
        this.openedFromExpedition = fromExpedition;
        this.expeditionLevel = expeditionLevel;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
