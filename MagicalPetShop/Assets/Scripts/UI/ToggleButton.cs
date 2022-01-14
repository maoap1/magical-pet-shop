using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{

    public Image image;
    public Sprite sprite1;
    public Sprite sprite2;
    public GameObject content1;
    public GameObject content2;

    private bool isContent1 = true;


    public void Toggle() {
        this.isContent1 = !this.isContent1;
        this.ShowContent();
    }

    public void ShowContent() {
        if (this.isContent1) {
            this.image.sprite = this.sprite1;
            this.content2.TweenAwareDisable();
            this.content1.TweenAwareEnable();
        } else {
            this.image.sprite = this.sprite2;
            this.content1.TweenAwareDisable();
            this.content2.TweenAwareEnable();
        }
    }

    // Start is called before the first frame update
    void Start() {
        this.isContent1 = true;
    }
}
