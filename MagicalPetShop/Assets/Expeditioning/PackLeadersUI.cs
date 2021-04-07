using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        // clear everything
        Clear();
        // display current info
        DisplayItems();
    }

    private void DisplayItems() {
        LeaderSlotUI leader = Instantiate(leaderSlot, this.leadersGrid.transform).GetComponent<LeaderSlotUI>();
        leader.Initialize(new Pack(), this.packOverviewUI);
        Pack tmp = new Pack(); tmp.owned = true;
        leader = Instantiate(leaderSlot, this.leadersGrid.transform).GetComponent<LeaderSlotUI>();
        leader.Initialize(tmp, this.packOverviewUI);
        for (int i = 0; i < 2; ++i) {
            LockedLeaderSlotUI lockedLeader = Instantiate(lockedLeaderSlot, this.leadersGrid.transform).GetComponent<LockedLeaderSlotUI>();
            lockedLeader.Initialize(new Pack());
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
