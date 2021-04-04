using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YardButton : MonoBehaviour {

    [SerializeField]
    Button button;

    PlayerState playerState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.playerState == null && PlayerState.THIS != null)
            this.playerState = PlayerState.THIS;
        if (this.playerState == null || this.playerState.level < 3) {
            this.button.interactable = false;
        } else {
            this.button.interactable = true;
        }
    }
}
