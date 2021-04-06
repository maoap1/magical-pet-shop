using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OngoingExpeditionUI : MonoBehaviour {

    public ExpeditionSummaryUI expeditionSummary;
    //public Expedition expedition;
    public bool clickable;

    public ProgressBar progressRing;
    public GameObject readyMessage;
    public Image expeditionImage;
    private bool finished;

    // Start is called before the first frame update
    void Start() {
        readyMessage.SetActive(false);
        progressRing.gameObject.SetActive(true);
        finished = true; // TODO: Just for testing, change to false afterwards
        //expeditionImage.sprite = expedition.expeditionType.artwork;
        //if (!PlayerState.THIS.expeditions.Contains(expedition)) {
        //    PlayerState.THIS.expeditions.Add(expedition);
        //}

    }

    // Update is called once per frame
    void Update() {
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
            // calculate result - number of artifacts, casualties
            // update inventory accordingly
            // show expedition summary (pass expedition result to it)
            this.expeditionSummary.Open(new ExpeditionResult());
            //PlayerState.THIS.expeditions.Remove(expedition);
            Destroy(this.gameObject);
        }
    }
}
