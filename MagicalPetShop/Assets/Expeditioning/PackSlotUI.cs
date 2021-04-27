using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackSlotUI : MonoBehaviour
{

    public GameObject highlight;

    [SerializeField]
    Image iconImage;
    [SerializeField]
    GameObject strengthInfo;
    [SerializeField]
    GameObject busyInfo;
    [SerializeField]
    Sprite badStrength;
    [SerializeField]
    Sprite neutralStrength;
    [SerializeField]
    Sprite goodStrength;


    [HideInInspector]
    public Pack pack;

    int expeditionLevel;

    SingleExpeditionUI singleExpeditionUI;
    PackOverviewUI packOverviewUI;

    public void Initialize(Pack pack, ExpeditionMode mode, int expeditionLevel, SingleExpeditionUI singleExpeditionUI, PackOverviewUI packOverviewUI) {
        this.pack = pack;
        this.expeditionLevel = expeditionLevel;
        this.singleExpeditionUI = singleExpeditionUI;
        this.packOverviewUI = packOverviewUI;
        this.iconImage.sprite = pack.artwork;
        if (pack.busy) {
            this.strengthInfo.SetActive(false);
            this.busyInfo.SetActive(true);
        } else {
            // TODO: Compare pack's strength to the required strength of the mode, choose sprite accordingly
            this.strengthInfo.GetComponent<Image>().sprite = this.neutralStrength;
            this.strengthInfo.SetActive(true);
            this.busyInfo.SetActive(false);
        }
    }

    public void ShowPackOverview() {
        this.packOverviewUI.Open(this.pack, true, this.expeditionLevel);
    }

    public void SelectPack() {
        if (!this.pack.busy)
            this.singleExpeditionUI.SelectPack(this);
    }

    public void Activate() {
        this.highlight.SetActive(true);
    }

    public void Deactivate() {
        this.highlight.SetActive(false);
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
