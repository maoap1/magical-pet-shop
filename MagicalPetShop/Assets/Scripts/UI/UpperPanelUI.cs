using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpperPanelUI : MonoBehaviour {

    [SerializeField]
    private GameObject settings;
    [SerializeField]
    private Text level;
    [SerializeField]
    private Text money;
    [SerializeField]
    private Text diamonds;
    [SerializeField]
    private SettingsButton settingsButton;

    private PlayerState playerState;

    // Start is called before the first frame update
    void Start()
    {
        settingsButton.SetSettings(this.settings);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.playerState == null && PlayerState.THIS != null)
            this.playerState = PlayerState.THIS;
        if (this.playerState == null) {
            this.level.text = "0";
            this.money.text = "0";
            this.diamonds.text = "0";
        } else {
            this.level.text = this.playerState.level.ToString();
            this.money.text = this.playerState.money.ToString();
            this.diamonds.text = this.playerState.diamonds.ToString();
        }
    }
}
