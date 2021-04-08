using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockedLeaderSlotUI : MonoBehaviour {

    [SerializeField]
    Image iconImage;
    [SerializeField]
    Text statusText;

    Pack pack;
    PackOverviewUI packOverviewUI;

    public void Initialize(Pack pack) {
        this.pack = pack;
        this.iconImage.sprite = pack.artworkSilhouette;
        this.statusText.text = "Unlock T" + pack.level.ToString() + " animal to acquire.";
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
