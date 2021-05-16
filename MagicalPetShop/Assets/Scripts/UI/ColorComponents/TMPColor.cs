using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMPColor : MonoBehaviour {
    public PaletteColor color;

    // Start is called before the first frame update
    void Start() {
        gameObject.GetComponent<TextMeshProUGUI>().color = UIPalette.THIS.GetColor(this.color);
    }
}
