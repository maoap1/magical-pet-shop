using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Shows information of one expedition type, allows to select difficulty, pack and to begin the expedition
[RequireComponent(typeof(AppearHideComponent))]
public class SingleExpeditionUI : MonoBehaviour {
    [SerializeField]
    PackOverviewUI packOverview;

    [SerializeField]
    TMPro.TextMeshProUGUI nameText;
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
    int expeditionIndex;
    ExpeditionDifficulty currentDifficulty;

    public void Open(ExpeditionType expedition) {
        GameLogic.THIS.inExpedition = true;
        this.expedition = expedition;
        this.expeditionIndex = GameLogic.THIS.expeditions.IndexOf(this.expedition);
        this.currentDifficulty = PlayerState.THIS.lastExpeditionDifficulties[this.expeditionIndex];
        this.activePack = null;
        Refresh();
        this.goButton.interactable = false;
        GetComponent<AppearHideComponent>().Do();
    }

    public void Close() {
        GameLogic.THIS.inExpedition = false;
        PlayerState.THIS.lastExpeditionDifficulties[this.expeditionIndex] = this.currentDifficulty;
        GetComponent<AppearHideComponent>().Revert();
    }
    public void OnEnable() {
        if (this.expedition != null) Refresh();
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
        if (Expeditioning.CanStartExpedition(this.activePack)) {
            this.goButton.interactable = true;
        }
    }

    public void StartExpedition() {
        Expeditioning.StartExpedition(this.activePack, this.expedition, this.currentDifficulty);
        PlayerState.THIS.lastExpeditionDifficulties[this.expeditionIndex] = this.currentDifficulty;
        this.activePack = null;
        Close();
    }

    private void Refresh() {
        // display correct data
        this.nameText.text = this.expedition.reward.name;
        // refresh difficulty details
        ExpeditionMode mode = this.expedition.difficultyModes[(int)this.currentDifficulty];
        this.expeditionModeUI.DisplayData(this.expedition, mode);
        // refresh pack leaders (first clear)
        int c = packsLayout.transform.childCount;
        for (int i = c - 1; i >= 0; i--)
            GameObject.Destroy(packsLayout.transform.GetChild(i).gameObject);
        PlayerState.THIS.packs.Sort((p1, p2) => p1.level.CompareTo(p2.level)); // TODO: Maybe sort by power and busyness, but it may be a little problematic
        foreach (Pack pack in PlayerState.THIS.packs) {
            if (pack.owned) {
                PackSlotUI newSlot = Instantiate(packSlot, this.packsLayout.transform).GetComponent<PackSlotUI>();
                newSlot.Initialize(pack, mode, this.expedition.level, this, this.packOverview);
                if (this.activePack == pack) {
                    newSlot.Activate();
                    if (Expeditioning.CanStartExpedition(this.activePack)) this.goButton.interactable = true;
                }
            }
        }
    }

}
