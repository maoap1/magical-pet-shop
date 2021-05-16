using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColor : MonoBehaviour {
    public PaletteColor color;

    // Start is called before the first frame update
    void Start() {
        gameObject.GetComponent<Text>().color = UIPalette.THIS.GetColor(this.color);
    }
}
