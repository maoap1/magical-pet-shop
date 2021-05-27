using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;


// Shows icon and progress of an ongoing expedition
public class OngoingExpeditionUI : MonoBehaviour, IPointerDownHandler {

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
        this.expeditionImage.sprite = expedition.expeditionType.reward.artwork;
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
            if (result.isSuccessful) {
                FindObjectOfType<AudioManager>().Play(SoundType.Success);
            } else {
                FindObjectOfType<AudioManager>().Play(SoundType.Fail);
            }
            Destroy(this.gameObject);
        } else {
            gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f);
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (finished) {
            gameObject.transform.DOScale(new Vector3(0.95f, 0.95f, 0.95f), 0.1f);
        }
    }

}
