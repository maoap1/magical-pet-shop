using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpeditionSlotUI : MonoBehaviour {

    [SerializeField]
    Image iconImage;
    [SerializeField]
    Text nameText;
    [SerializeField]
    Image rewardImage;
    [SerializeField]
    Text durationText;

    private ExpeditionType expedition;
    private SingleExpeditionUI expeditionUI;

    public void Initialize(ExpeditionType expedition, SingleExpeditionUI expeditionUI) {
        this.expedition = expedition;
        this.expeditionUI = expeditionUI;

        this.iconImage.sprite = expedition.artwork;
        this.nameText.text = expedition.name;
        this.rewardImage.sprite = expedition.reward.artwork;
        this.durationText.text = expedition.GetFormattedDuration();
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
