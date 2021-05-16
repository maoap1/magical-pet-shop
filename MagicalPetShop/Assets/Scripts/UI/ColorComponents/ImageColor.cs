using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageColor : MonoBehaviour {
    public PaletteColor color;
    public bool initializeOnStart = true;

    // Start is called before the first frame update
    void Start() {
        if (this.initializeOnStart)
            gameObject.GetComponent<Image>().color = UIPalette.THIS.GetColor(this.color);
    }
}
