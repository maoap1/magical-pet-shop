using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Shows summary of expedition results - number of artifacts, casualties
[RequireComponent(typeof(AppearHideComponent))]
public class ExpeditionSummaryUI : MonoBehaviour {
    [SerializeField]
    List<GameObject> objectsToHideFromUpper;
    [SerializeField]
    Text resultText;
    [SerializeField]
    Image rewardImage;
    [SerializeField]
    Text rewardCountText;
    [SerializeField]
    Text noCasualtiesText;
    [SerializeField]
    GameObject casualtySlot;
    [SerializeField]
    HorizontalLayoutGroup casualtiesLayout;

    bool openedFromUpperPanel;

    public void Open(ExpeditionResult result, bool fromUpper = false) {
        this.openedFromUpperPanel = fromUpper;
        DisplayData(result);
        this.gameObject.SetActive(true);
        if (!fromUpper) {
            foreach (GameObject g in GetComponent<AppearHideComponent>().ObjectsToAppear) {
                g.SetActive(true);
            }
        }
        if (!fromUpper) {
            foreach (GameObject g in GetComponent<AppearHideComponent>().ObjectsToHide) {
                g.SetActive(false);
            }
        } else {
            foreach (GameObject g in objectsToHideFromUpper) {
                g.SetActive(false);
            }
        }
    }

    public void Close() {
        this.gameObject.SetActive(false);
        if (!openedFromUpperPanel) {
            foreach (GameObject g in GetComponent<AppearHideComponent>().ObjectsToAppear) {
                g.SetActive(false);
            }
        }
        if (!openedFromUpperPanel) {
            foreach (GameObject g in GetComponent<AppearHideComponent>().ObjectsToHide) {
                g.SetActive(true);
            }
        } else {
            foreach (GameObject g in objectsToHideFromUpper) {
                g.SetActive(true);
            }
        }
    }

    private void DisplayData(ExpeditionResult result) {
        this.resultText.text = result.isSuccessful ? "SUCCESS" : "FAIL";
        this.rewardImage.sprite = result.reward.artifact.artwork;
        this.rewardCountText.text = "+" + result.reward.count.ToString();
        this.noCasualtiesText.gameObject.SetActive(result.casualties.Count == 0);

        // Display casualties (first clear)
        int c = this.casualtiesLayout.transform.childCount;
        for (int i = c - 1; i >= 0; i--)
            GameObject.Destroy(this.casualtiesLayout.transform.GetChild(i).gameObject);
        foreach (InventoryAnimal animal in result.casualties) {
            CasualtyIconUI newSlot = Instantiate(this.casualtySlot, this.casualtiesLayout.transform).GetComponent<CasualtyIconUI>();
            newSlot.Initialize(animal);
        }
    }
}
