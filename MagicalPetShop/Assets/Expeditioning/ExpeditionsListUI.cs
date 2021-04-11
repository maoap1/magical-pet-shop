using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpeditionsListUI : MonoBehaviour {

    [SerializeField]
    private SingleExpeditionUI singleExpeditionUI;
    [SerializeField]
    List<GameObject> objectsToHide;
    [SerializeField]
    List<GameObject> objectsToAppear;
    [SerializeField]
    private GameObject expeditionSlot;
    [SerializeField]
    VerticalLayoutGroup layout;

    public void Open(int level) {
        DisplayItems(level);
        this.gameObject.SetActive(true);
        foreach (GameObject g in objectsToAppear) {
            g.SetActive(true);
        }
        foreach (GameObject g in objectsToHide) {
            g.SetActive(false);
        }
    }

    public void Close() {
        this.gameObject.SetActive(false);
        foreach (GameObject g in objectsToAppear) {
            g.SetActive(false);
        }
        foreach (GameObject g in objectsToHide) {
            g.SetActive(true);
        }
    }

    private void DisplayItems(int level) {
        Clear();
        // Display expeditions of the given level
        // TODO: Maybe sort them somehow?
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
