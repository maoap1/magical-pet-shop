using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour {

    void Start() {
        Button button = gameObject.GetComponent<Button>();
        if (button != null) {
            button.onClick.AddListener(ClickedSound);
        }
    }

    public void ClickedSound() {
        FindObjectOfType<AudioManager>().Play(SoundType.Click);
    }

}
