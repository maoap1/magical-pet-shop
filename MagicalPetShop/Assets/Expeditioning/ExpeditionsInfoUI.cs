using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpeditionsInfoUI : MonoBehaviour {

    [SerializeField]
    bool clickable;
    [SerializeField]
    ExpeditionSummaryUI expeditionSummary;
    [SerializeField]
    GameObject expeditionPrefab;
    [SerializeField]
    float updateTime;

    // Start is called before the first frame update
    void Start()
    {
        OngoingExpeditionUI display = Instantiate(expeditionPrefab, this.transform).GetComponent<OngoingExpeditionUI>();
        display.expeditionSummary = this.expeditionSummary;
        display.clickable = this.clickable;
    }

    // Update is called once per frame
    void Update() {
        /*
        if (Time.time - updateTime > 0.1) {
            updateTime = Time.time;
            List<OngoingExpeditionUI> displays = new List<OngoingExpeditionUI>(gameObject.GetComponentsInChildren<OngoingExpeditionUI>());
            foreach (Expedition expedition in PlayerState.THIS.expeditions) {
                if (displays.Find(x => x.expedition == expedition) == null) {
                    OngoingExpeditionUI display = Instantiate(expeditionPrefab, this.transform).GetComponent<OngoingExpeditionUI>();
                    display.expedition = expedition; // TODO: or pass it through a method?
                }
            }
        }
        */
    }
}
