using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Shows summary of expedition results - number of artifacts, casualties
[RequireComponent(typeof(AppearHideComponent))]
public class ExpeditionSummaryUI : MonoBehaviour {
    public GameObject CrownHat;
    public GameObject JokerHat;

    [SerializeField]
    List<GameObject> objectsToHideFromUpper;
    [SerializeField]
    TMPro.TextMeshProUGUI resultText;
    [SerializeField]
    Image leaderImage;
    [SerializeField]
    Image rewardImage;
    [SerializeField]
    TMPro.TextMeshProUGUI rewardCountText;
    [SerializeField]
    TMPro.TextMeshProUGUI noCasualtiesText;
    [SerializeField]
    GameObject casualtySlot;
    [SerializeField]
    HorizontalLayoutGroup casualtiesLayout;

    bool openedFromUpperPanel;

    public void Open(ExpeditionResult result, bool fromUpper = false) {
        this.openedFromUpperPanel = fromUpper;
        DisplayData(result);
        if (!fromUpper)
        {
            GetComponent<AppearHideComponent>().Do();
        }
        else 
        {
            gameObject.TweenAwareEnable();
            foreach (GameObject g in objectsToHideFromUpper) 
            {
                g.TweenAwareDisable();
            }
        }
    }

    public void Close() {
        if (!openedFromUpperPanel) 
        {
            GetComponent<AppearHideComponent>().Revert();
        } 
        else 
        {
            gameObject.TweenAwareDisable();
            foreach (GameObject g in objectsToHideFromUpper) 
            {
                g.TweenAwareEnable();
            }
        }
    }

    private void DisplayData(ExpeditionResult result) {
        this.resultText.text = result.isSuccessful ? "SUCCESS!" : "FAILURE!";
        JokerHat.SetActive(!result.isSuccessful);
        CrownHat.SetActive(result.isSuccessful);
        this.leaderImage.sprite = result.pack.artwork;
        this.rewardImage.sprite = result.reward.artifact.artwork;
        this.rewardCountText.text = result.reward.count.ToString();
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
