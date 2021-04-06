using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderSlotUI : MonoBehaviour {

    [SerializeField]
    GameObject power;
    [SerializeField]
    GameObject status;
    [SerializeField]
    GameObject cost;

    PackLeader leader;
    PackOverviewUI packOverviewUI;


    public void Initialize(PackLeader leader, PackOverviewUI packOverviewUI) {
        this.leader = leader;
        this.packOverviewUI = packOverviewUI;
        if (leader.owned) {
            this.power.SetActive(true);
            this.status.SetActive(true);
            this.cost.SetActive(false);
        } else {
            this.power.SetActive(false);
            this.status.SetActive(false);
            this.cost.SetActive(true);
        }
    }

    public void Clicked() {
        // TODO: If the leader is not owned, buy him, create a new pack for him, otherwise just find his pack
        // if (!this.leader.owned) ...
        this.packOverviewUI.Open(new Pack(), false);
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
