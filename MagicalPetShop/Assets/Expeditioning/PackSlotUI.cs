using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackSlotUI : MonoBehaviour
{

    public GameObject highlight;

    [HideInInspector]
    public Pack pack;

    SingleExpeditionUI singleExpeditionUI;
    PackOverviewUI packOverviewUI;

    public void Initialize(Pack pack, SingleExpeditionUI singleExpeditionUI, PackOverviewUI packOverviewUI) {
        this.pack = pack;
        this.singleExpeditionUI = singleExpeditionUI;
        this.packOverviewUI = packOverviewUI;
    }

    public void ShowPackOverview() {
        Debug.Log("ShowPackOverview");
        this.packOverviewUI.Open(this.pack, true);
    }

    public void SelectPack() {
        Debug.Log("SelectPack");
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
