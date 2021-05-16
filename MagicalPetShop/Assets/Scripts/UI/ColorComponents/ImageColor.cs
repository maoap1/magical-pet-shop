using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageColor : MonoBehaviour {
    public PaletteColor color;

    // Start is called before the first frame update
    void Start() {
        gameObject.GetComponent<Image>().color = UIPalette.THIS.GetColor(this.color);
    }
}
