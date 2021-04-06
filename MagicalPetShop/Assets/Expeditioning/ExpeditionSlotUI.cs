using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpeditionSlotUI : MonoBehaviour
{
    private ExpeditionType expedition;
    private SingleExpeditionUI expeditionUI;

    public void Initialize(ExpeditionType expedition, SingleExpeditionUI expeditionUI) {
        this.expedition = expedition;
        this.expeditionUI = expeditionUI;
    }

    public void Clicked() {
        this.expeditionUI.Open(this.expedition);
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
