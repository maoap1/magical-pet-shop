using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{

    private CheatsUI cheats;

    void Start() {
    }

    public void ShowSettings() {
        FindObjectOfType<Settings>().Open();
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
