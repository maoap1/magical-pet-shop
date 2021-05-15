using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NavBarSceneButton : MonoBehaviour {

    public string sceneName;
    public int fromLevel;
    public Sprite selectedIcon;
    public Image image;
    public Material grayscale;

    private bool isActive = false;
    private bool isUnlocked = false;
    private Button button;

    private NavBarButton navBarButton;


    public void LoadScene() {
        if (!this.isActive) {
            GameObject.FindObjectOfType<SceneSwitcher>().LoadScene(sceneName);
        }
    }

    public void ShowNotification() {
        if (!this.isActive) {
            this.navBarButton.ShowNotification();
        }
    }
    
    public void HideNotification() {
        this.navBarButton.HideNotification();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.button = gameObject.GetComponent<Button>();
        this.button.interactable = false;
        // initialize everything to grey
        this.image.material = new Material(this.grayscale);
        this.image.material.SetFloat("_GrayscaleAmount", 1);
        this.navBarButton = gameObject.GetComponent<NavBarButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerState.THIS.level < this.fromLevel) {
            this.button.interactable = false;
        } else {
            this.button.interactable = true;
        }

        
        if (!this.isUnlocked && PlayerState.THIS.level >= this.fromLevel) {
            // activate, change colors to normal
            this.isUnlocked = true;
            this.button.interactable = true;
            this.image.material.SetFloat("_GrayscaleAmount", 0);
        }

        if (this.isUnlocked && !this.isActive && SceneManager.GetActiveScene().name == this.sceneName) {
            this.isActive = true;
            this.image.sprite = this.selectedIcon;
        }
        if (this.isUnlocked && this.isActive && SceneManager.GetActiveScene().name != this.sceneName) {
            this.isActive = false;
            this.image.sprite = this.navBarButton.icon;
        }
    }
}
