using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Shows basic information about an expedition type
public class ExpeditionSlotUI : MonoBehaviour {

    [SerializeField]
    Image iconImage;
    [SerializeField]
    TMPro.TextMeshProUGUI nameText;
    [SerializeField]
    //TMPro.TextMeshProUGUI durationText;

    private ExpeditionType expedition;
    private SingleExpeditionUI expeditionUI;

    public void Initialize(ExpeditionType expedition, SingleExpeditionUI expeditionUI) {
        this.expedition = expedition;
        this.expeditionUI = expeditionUI;

        this.iconImage.sprite = expedition.reward.artwork;
        this.nameText.text = expedition.reward.name;
        //this.durationText.text = expedition.GetFormattedDuration();
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
