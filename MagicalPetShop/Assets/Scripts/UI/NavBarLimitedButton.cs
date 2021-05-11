using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavBarLimitedButton : MonoBehaviour
{
    public int fromLevel;
    public Image icon;
    public Material grayscale;

    private bool isActive = false;
    private Button button;
    private Image background;
    private PlayerState playerState;
    private Color originalColor;

    public void Clicked() {
        if (!this.isActive) return;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.button = gameObject.GetComponent<Button>();
        this.background = gameObject.GetComponent<Image>();
        this.button.interactable = false;
        // initialize everything to grey
        this.originalColor = this.background.color;
        this.background.color = Color.gray;
        this.icon.material = new Material(this.grayscale);
        this.icon.material.SetFloat("_GrayscaleAmount", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.playerState == null && PlayerState.THIS != null)
            this.playerState = PlayerState.THIS;
        if (!this.isActive && this.playerState != null && this.playerState.level >= this.fromLevel) {
            // activate, change colors to normal
            this.isActive = true;
            this.button.interactable = true;
            this.background.color = this.originalColor;
            this.icon.material.SetFloat("_GrayscaleAmount", 0);
        }
    }
}
