using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [SerializeField]
    private GameObject settings;

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
        if (this.cheats != null) {
            this.cheats.ToggleVisibility();
            return;
        }
        this.cheats = FindObjectOfType<CheatsUI>();
        if (this.cheats != null) {
            this.cheats.ToggleVisibility();
            return;
        }
        List<CheatsUI> cheatsTmp = Utils.FindObject<CheatsUI>();
        if (cheatsTmp.Count > 0) {
            this.cheats = cheatsTmp[0];
            this.cheats.ToggleVisibility();
        } else Debug.Log("Cheats not found");
    }
}
