using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Shows overview of pack leaders - their icon, status, power, ...
public class PackLeadersUI : MonoBehaviour {

    [SerializeField]
    List<GameObject> objectsToHide;
    [SerializeField]
    List<GameObject> objectsToAppear;
    [SerializeField]
    PackOverviewUI packOverviewUI;
    [SerializeField]
    GridLayoutGroup leadersGrid;
    [SerializeField]
    LeaderSlotUI leaderSlot;
    [SerializeField]
    LockedLeaderSlotUI lockedLeaderSlot;

    public void Open() {
        Refresh();
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

    public void Refresh() {
        Clear();
        DisplayItems();
    }

    private void DisplayItems() {
        // display all unlocked leaders
        List<Pack> packs = PacksManager.GetPacks();
        packs.Sort((p1, p2) => p1.level.CompareTo(p2.level));
        foreach (Pack pack in packs) {
            if (pack.unlocked) {
                LeaderSlotUI leader = Instantiate(leaderSlot, this.leadersGrid.transform).GetComponent<LeaderSlotUI>();
                leader.Initialize(pack, this.packOverviewUI);
            }
        }
        // then display the first one locked (if available)
        foreach (Pack pack in packs) {
            if (!pack.unlocked) {
                LockedLeaderSlotUI lockedLeader = Instantiate(lockedLeaderSlot, this.leadersGrid.transform).GetComponent<LockedLeaderSlotUI>();
                lockedLeader.Initialize(pack);
                break;
            }
        }
    }

    private void Clear() {
        int c = leadersGrid.transform.childCount;
        for (int i = c - 1; i >= 0; i--)
            GameObject.Destroy(leadersGrid.transform.GetChild(i).gameObject);
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
