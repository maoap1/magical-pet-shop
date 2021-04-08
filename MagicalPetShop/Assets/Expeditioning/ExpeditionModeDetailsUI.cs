using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpeditionModeDetailsUI : MonoBehaviour
{
    [SerializeField]
    Text difficultyText;
    [SerializeField]
    Image rewardImage;
    [SerializeField]
    Text rewardCountText;
    [SerializeField]
    Text durationText;

    public void DisplayData(ExpeditionType expeditionType, ExpeditionMode mode) {
        this.difficultyText.text = mode.difficulty.ToString();
        this.rewardImage.sprite = expeditionType.reward.artwork;
        this.rewardCountText.text = mode.minRewardCount + "-" + mode.maxRewardCount;
        this.durationText.text = expeditionType.GetFormattedDuration();
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
