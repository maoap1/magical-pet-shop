using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMPColor : MonoBehaviour {
    public PaletteColor color;
    public bool initializeOnStart = true;

    // Start is called before the first frame update
    void Start() {
        if (this.initializeOnStart)
            gameObject.GetComponent<TextMeshProUGUI>().color = UIPalette.THIS.GetColor(this.color);
    }
}
