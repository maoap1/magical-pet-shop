using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Shows a list of expeditions of a specific level
[RequireComponent(typeof(AppearHideComponent))]
public class ExpeditionsListUI : MonoBehaviour {

    [SerializeField]
    private SingleExpeditionUI singleExpeditionUI;
    [SerializeField]
    private GameObject expeditionSlot;
    [SerializeField]
    VerticalLayoutGroup layout;

    public void Open(int level) {
        GameLogic.THIS.inExpeditionList = true;
        DisplayItems(level);
        GetComponent<AppearHideComponent>().Do();
    }

    public void Close() {
        GameLogic.THIS.inExpeditionList = false;
        GetComponent<AppearHideComponent>().Revert();
    }

    private void DisplayItems(int level) {
        Clear();
        // Display expeditions of the given level, sorted by duration
        GameLogic.THIS.expeditions.Sort((e1, e2) => e1.duration.CompareTo(e2.duration));
        foreach (ExpeditionType expedition in GameLogic.THIS.expeditions) {
            if (expedition.level == level) {
                ExpeditionSlotUI newSlot = Instantiate(expeditionSlot, this.layout.transform).GetComponent<ExpeditionSlotUI>();
                newSlot.Initialize(expedition, this.singleExpeditionUI);
            }
        }
    }

    private void Clear() {
        int c = this.layout.transform.childCount;
        for (int i = c - 1; i >= 0; i--)
            GameObject.Destroy(layout.transform.GetChild(i).gameObject);
    }
}
