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
            // TODO: Displayed level should correspond to the player's level (for now, the highest level of player's animals is displayed)
            int maxLevel = 0;
            if (this.playerState.animals == null)
                maxLevel = 0;
            else {
                foreach (InventoryAnimal animal in this.playerState.animals) {
                    if (animal.animal.level > maxLevel) maxLevel = animal.animal.level;
                }
            }
            this.level.text = maxLevel.ToString();
            this.money.text = PlayerState.THIS.money.ToString();
            this.diamonds.text = PlayerState.THIS.diamonds.ToString();
        }
    }
}
