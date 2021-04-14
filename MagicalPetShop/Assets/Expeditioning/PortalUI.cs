using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Represents a portal in Yard scene
// May be locked of player's level is too low
// Shows list of expeditions on click
public class PortalUI : MonoBehaviour
{
    [SerializeField]
    private int level;
    [SerializeField]
    private GameObject lockImage;
    [SerializeField]
    private Button portalButton;

    private bool locked;

    private ExpeditionsListUI expeditionsList;

    public void SetExpeditionList(ExpeditionsListUI expeditionsList) {
        this.expeditionsList = expeditionsList;
    }

    public void ShowExpeditions() {
        this.expeditionsList.Open(this.level);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.locked = true;
        this.lockImage.SetActive(true);
        this.portalButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.locked && PlayerState.THIS.level >= this.level) {
            this.locked = false;
            this.lockImage.SetActive(false);
            this.portalButton.interactable = true;
        }
    }
}
