using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpeditionSummaryUI : MonoBehaviour {

    [SerializeField]
    List<GameObject> objectsToHide;
    [SerializeField]
    List<GameObject> objectsToAppear;

    [SerializeField]
    Text resultText;
    [SerializeField]
    Image rewardImage;
    [SerializeField]
    Text rewardCountText;
    [SerializeField]
    GameObject casualtySlot;
    [SerializeField]
    HorizontalLayoutGroup casualtiesLayout;

    public void Open(ExpeditionResult result) {
        DisplayData(result);
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

    private void DisplayData(ExpeditionResult result) {
        this.resultText.text = result.isSuccessful ? "SUCCESS" : "FAIL";
        this.rewardImage.sprite = result.reward.artwork;
        this.rewardCountText.text = "+" + result.rewardCount.ToString();

        // Display casualties (first clear)
        int c = this.casualtiesLayout.transform.childCount;
        for (int i = c - 1; i >= 0; i--)
            GameObject.Destroy(this.casualtiesLayout.transform.GetChild(i).gameObject);
        foreach (Animal animal in result.casualties) {
            CasualtyIconUI newSlot = Instantiate(this.casualtySlot, this.casualtiesLayout.transform).GetComponent<CasualtyIconUI>();
            newSlot.Initialize(animal);
        }
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
