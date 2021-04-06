using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpeditionsInfo : MonoBehaviour {

    public GameObject expeditionPrefab;
    public float updateTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        /*
        if (Time.time - updateTime > 1) {
            updateTime = Time.time;
            List<OngoingExpedition> displays = new List<OngoingExpedition>(gameObject.GetComponentsInChildren<OngoingExpedition>());
            foreach (Expedition expedition in PlayerState.THIS.expeditions) {
                if (displays.Find(x => x.expedition == expedition) == null) {
                    OngoingExpedition display = Instantiate(expeditionPrefab, this.transform).GetComponent<OngoingExpedition>();
                    display.expedition = expedition; // TODO: or pass it through a method?
                }
            }
        }
        */
    }
}
