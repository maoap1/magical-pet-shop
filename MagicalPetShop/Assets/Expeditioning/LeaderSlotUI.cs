using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderSlotUI : MonoBehaviour {

    [SerializeField]
    Image iconImage;
    [SerializeField]
    Text nameText;
    [SerializeField]
    Text powerText;
    [SerializeField]
    Text statusText;
    [SerializeField]
    Text costText;

    [SerializeField]
    GameObject power;
    [SerializeField]
    GameObject status;
    [SerializeField]
    GameObject cost;

    Pack pack;
    PackOverviewUI packOverviewUI;


    public void Initialize(Pack pack, PackOverviewUI packOverviewUI) {
        this.pack = pack;
        this.packOverviewUI = packOverviewUI;
        Refresh();
    }

    public void Clicked() {
        // If the pack is not owned, buy it
        if (this.pack.unlocked && !this.pack.owned && Inventory.HasInInventory(this.pack.cost)) {
            Inventory.TakeFromInventory(this.pack.cost);
            this.pack.owned = true;
            Refresh();
        }
        if (this.pack.owned) {
            this.packOverviewUI.Open(this.pack);
        }
    }

    public void Refresh() {
        this.iconImage.sprite = this.pack.artwork;
        this.nameText.text = this.pack.name;
        if (pack.owned) {
            this.powerText.text = this.pack.GetTotalPower().ToString();
            this.power.SetActive(true);
            this.statusText.text = this.pack.busy ? "EXPLORING" : "FREE";
            this.status.SetActive(true);
            this.cost.SetActive(false);
        } else {
            this.power.SetActive(false);
            this.status.SetActive(false);
            this.costText.text = this.pack.cost.ToString();
            if (Inventory.HasInInventory(this.pack.cost)) this.costText.color = Color.black;
            else this.costText.color = Color.red;
            this.cost.SetActive(true);
        }
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
