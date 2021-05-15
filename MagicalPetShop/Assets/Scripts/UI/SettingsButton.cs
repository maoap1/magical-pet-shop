using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [SerializeField]
    private GameObject settings;
    [SerializeField]
    private CheatsUI cheats;

    void Start() {
    }

    public void SetSettings(GameObject settings) {
        this.settings = settings;
    }

    public void ShowSettings() {
        this.settings.SetActive(true);
    }

    public void ShowCheats() {
        cheats.ToggleVisibility();
    }
}
