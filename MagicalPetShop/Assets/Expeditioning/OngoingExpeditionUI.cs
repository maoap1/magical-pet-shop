using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OngoingExpeditionUI : MonoBehaviour {

    public ExpeditionSummaryUI expeditionSummary;
    public Expedition expedition;
    public bool clickable;

    public ProgressBar progressRing;
    public GameObject readyMessage;
    public Image expeditionImage;
    private bool finished;

    public void Initialize(Expedition expedition, bool clickable, ExpeditionSummaryUI expeditionSummary) {
        this.expedition = expedition;
        this.clickable = clickable;
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
        // TODO
        /*
        if (!finished) {
            if (expedition.fillRate >= 1) {
                finished = true;
                progressRing.gameObject.SetActive(false);
                readyMessage.SetActive(true);
            } else {
                progressRing.fillRate = expedition.fillRate;
            }
        }
        if (PlayerState.THIS.expeditions.Find(x => x == expedition) == null) {
            Destroy(this.gameObject);
        }
        */
    }

    public void Clicked() {
        if (finished && clickable) {
            // TODO
            // calculate result - number of artifacts, casualties
            // update inventory accordingly
            // show expedition summary (pass expedition result to it)
            this.expeditionSummary.Open(new ExpeditionResult());
            //PlayerState.THIS.expeditions.Remove(this.expedition);
            Destroy(this.gameObject);
        }
    }
}
