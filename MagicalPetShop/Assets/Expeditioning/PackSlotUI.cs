using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Shows icon and relative power of a specific pack, may be selected for an expedition
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
        this.iconImage.material = new Material(this.iconImage.material);
        if (pack.busy) {
            this.iconImage.material.SetFloat("_GrayscaleAmount", 1);
            this.strengthInfo.SetActive(false);
            this.busyInfo.SetActive(true);
        } else {
            this.iconImage.material.SetFloat("_GrayscaleAmount", 0);
            // Compare pack's power to the required power of the mode, choose sprite accordingly
            int power = pack.GetTotalPower();
            if (power < mode.requiredPower.fortyPercent) {
                this.strengthInfo.GetComponent<Image>().sprite = this.badStrength;
            } else if (power < mode.requiredPower.basePower) {
                this.strengthInfo.GetComponent<Image>().sprite = this.neutralStrength;
            } else {
                this.strengthInfo.GetComponent<Image>().sprite = this.goodStrength;
            }
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
