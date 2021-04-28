using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Used in pack leaders overview - to buy a leader or to show pack overview
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

    public void OnEnable() {
        if (this.pack != null) Refresh();
    }

    public void Clicked() {
        // If the pack is not owned, buy it
        if (!this.pack.owned) { 
            if (PacksManager.BuyPack(this.pack))
                Refresh();
        }
        if (this.pack.owned) {
            this.packOverviewUI.Open(this.pack);
        }
    }

    public void Refresh() {
        this.iconImage.sprite = this.pack.artwork;
        this.nameText.text = this.pack.name;
        this.iconImage.material = new Material(this.iconImage.material);
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
            if (Inventory.HasInInventory(this.pack.cost)) {
                this.costText.color = Color.black;
                this.iconImage.material.SetFloat("_GrayscaleAmount", 0);
            } else {
                this.costText.color = Color.red;
                this.iconImage.material.SetFloat("_GrayscaleAmount", 1);
            }
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
