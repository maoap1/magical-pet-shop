using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpperPanelUI : MonoBehaviour {

    [SerializeField]
    private TMPro.TextMeshProUGUI level;
    [SerializeField]
    private TMPro.TextMeshProUGUI money;
    [SerializeField]
    private TMPro.TextMeshProUGUI diamonds;
    [SerializeField]
    private SettingsButton settingsButton;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.level.text = PlayerState.THIS.level.ToString();
        this.diamonds.text = PlayerState.THIS.diamonds.ToString();
    }
}
