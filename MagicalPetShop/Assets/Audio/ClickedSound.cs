using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickedSound : MonoBehaviour {

    public SoundType soundType = SoundType.Click;

    void Start() {
        Button button = gameObject.GetComponent<Button>();
        if (button != null) {
            button.onClick.AddListener(PlaySound);
        }
    }

    public void PlaySound() {
        FindObjectOfType<AudioManager>().Play(this.soundType);
    }

}
