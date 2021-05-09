using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Shows icon and progress of an ongoing expedition
public class OngoingExpeditionUI : MonoBehaviour {

    public ExpeditionSummaryUI expeditionSummary;
    public Expedition expedition;
    public bool isUpperPanel;

    public ProgressBar progressRing;
    public GameObject readyMessage;
    public Image expeditionImage;
    private bool finished;

    public void Initialize(Expedition expedition, bool isUpperPanel, ExpeditionSummaryUI expeditionSummary) {
        this.expedition = expedition;
        this.isUpperPanel = isUpperPanel;
        this.expeditionSummary = expeditionSummary;
        this.expeditionImage.sprite = expedition.expeditionType.artwork;
        if (!PlayerState.THIS.expeditions.Contains(expedition)) {
            PlayerState.THIS.expeditions.Add(expedition);
        }
    }

    // Start is called before the first frame update
    void Start() {
        readyMessage.SetActive(false);
        progressRing.gameObject.SetActive(true);
        finished = false;
    }

    // Update is called once per frame
    void Update() {
        if (!finished) {
            if (expedition.fillRate >= 1) {
                finished = true;
                progressRing.gameObject.SetActive(false);
                readyMessage.SetActive(true);
            } else {
                readyMessage.SetActive(false);
                progressRing.fillRate = expedition.fillRate;
            }
        }
        if (PlayerState.THIS.expeditions.Find(x => x == expedition) == null) {
            Destroy(this.gameObject);
        }
    }

    public void Clicked() {
        if (finished) {
            ExpeditionResult result = Expeditioning.FinishExpedition(this.expedition);
            this.expeditionSummary.Open(result, this.isUpperPanel);
            Destroy(this.gameObject);
        }
    }
}
