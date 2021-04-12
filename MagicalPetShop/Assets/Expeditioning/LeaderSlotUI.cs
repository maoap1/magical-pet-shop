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

        this.iconImage.sprite = pack.artwork;
        this.nameText.text = pack.name;
        if (pack.owned) {
            this.powerText.text = pack.GetTotalPower().ToString();
            this.power.SetActive(true);
            this.statusText.text = pack.busy ? "EXPLORING" : "FREE";
            this.status.SetActive(true);
            this.cost.SetActive(false);
        } else {
            this.power.SetActive(false);
            this.status.SetActive(false);
            this.costText.text = pack.cost.ToString();
            this.cost.SetActive(true);
        }
    }

    public void Clicked() {
        // TODO: If the pack is not owned, buy it
        // if (!this.pack.owned) ...
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
