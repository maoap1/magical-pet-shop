using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleExpeditionUI : MonoBehaviour {

    [SerializeField]
    List<GameObject> objectsToHide;
    [SerializeField]
    List<GameObject> objectsToAppear;

    [SerializeField]
    PackOverviewUI packOverview;

    [SerializeField]
    Image iconImage;
    [SerializeField]
    Text nameText;
    [SerializeField]
    Button goButton;
    [SerializeField]
    ExpeditionModeDetailsUI expeditionModeUI;
    [SerializeField]
    PackSlotUI packSlot;
    [SerializeField]
    HorizontalLayoutGroup packsLayout;

    Pack activePack;
    ExpeditionType expedition;
    ExpeditionDifficulty currentDifficulty;

    public void Open(ExpeditionType expedition) {
        this.expedition = expedition;
        this.currentDifficulty = ExpeditionDifficulty.Medium;
        Refresh();
        this.goButton.interactable = false;
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

    public void ChangeDifficulty(bool right) {
        // determine new difficulty
        if (right) {
            int tmp = (int)this.currentDifficulty + 1;
            if (tmp > (int)ExpeditionDifficulty.Hard) tmp = 0;
            this.currentDifficulty = (ExpeditionDifficulty)tmp;
        } else {
            int tmp = (int)this.currentDifficulty - 1;
            if (tmp < 0) this.currentDifficulty = ExpeditionDifficulty.Hard;
            else this.currentDifficulty = (ExpeditionDifficulty)tmp;
        }
        Debug.Log("Current difficulty: " + this.currentDifficulty);
        // display correct data
        Refresh();
    }

    public void SelectPack(PackSlotUI pack) {
        List<PackSlotUI> packs = new List<PackSlotUI>(this.packsLayout.GetComponentsInChildren<PackSlotUI>());
        foreach (PackSlotUI p in packs) {
            p.Deactivate();
        }
        pack.Activate();
        this.activePack = pack.pack;
        this.goButton.interactable = true;
    }

    public void StartExpedition() {
        // TODO: start expedition according to the active pack and current difficulty
        Debug.Log("Starting expedition...");
        Close();
    }

    private void Refresh() {
        // display correct data
        this.iconImage.sprite = this.expedition.artwork;
        this.nameText.text = this.expedition.name;
        // refresh difficulty details
        ExpeditionMode mode = this.expedition.difficultyModes[(int)this.currentDifficulty];
        this.expeditionModeUI.DisplayData(this.expedition, mode);
        // refresh pack leaders (first clear)
        int c = packsLayout.transform.childCount;
        for (int i = c - 1; i >= 0; i--)
            GameObject.Destroy(packsLayout.transform.GetChild(i).gameObject);
        foreach (Pack pack in PlayerState.THIS.packs) {
            if (pack.owned) {
                PackSlotUI newSlot = Instantiate(packSlot, this.packsLayout.transform).GetComponent<PackSlotUI>();
                newSlot.Initialize(pack, mode, this.expedition.level, this, this.packOverview);
            }
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
