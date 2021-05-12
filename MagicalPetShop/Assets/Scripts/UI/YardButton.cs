using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class YardButton : MonoBehaviour {

    public string sceneName;
    public int fromLevel;
    public Image icon;
    public Material grayscale;

    private bool isActive = false;
    private bool isUnlocked = false;
    private Button button;
    private Image background;
    private PlayerState playerState;

    Dictionary<string, SoundType> transitionSounds = new Dictionary<string, SoundType>() {
        { "Lab", SoundType.Steps },
        { "Shop", SoundType.CustomerAppear },
        { "Yard", SoundType.Door }
    };

    public void LoadScene() {
        if (!this.isActive) {
            if (transitionSounds.ContainsKey(this.sceneName)) {
                FindObjectOfType<AudioManager>().Play(transitionSounds[this.sceneName]);
            }
            SceneManager.LoadScene(sceneName);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.button = gameObject.GetComponent<Button>();
        this.background = gameObject.GetComponent<Image>();
        this.button.interactable = false;
        // initialize everything to grey
        this.background.color = Color.gray;
        this.icon.material = new Material(this.grayscale);
        this.icon.material.SetFloat("_GrayscaleAmount", 1);
    }

    // Update is called once per frame
    void Update() {
        if (this.playerState == null && PlayerState.THIS != null)
            this.playerState = PlayerState.THIS;
        if (this.playerState == null || this.playerState.level < 3) {
            this.button.interactable = false;
        } else {
            this.button.interactable = true;
        }

        if (this.playerState == null && PlayerState.THIS != null)
            this.playerState = PlayerState.THIS;
        if (!this.isUnlocked && this.playerState != null && this.playerState.level >= this.fromLevel) {
            // activate, change colors to normal
            this.isUnlocked = true;
            this.button.interactable = true;
            // TODO: Use color from pallete
            this.background.color = new Color(0.4118f, 0f, 0.5882f); // dark violet
            this.icon.material.SetFloat("_GrayscaleAmount", 0);
        }

        if (this.isUnlocked && !this.isActive && SceneManager.GetActiveScene().name == this.sceneName) {
            this.isActive = true;
            // TODO: Use color from pallete
            this.background.color = new Color(0.651f, 0.486f, 0f); // dark gold
        }
        if (this.isUnlocked && this.isActive && SceneManager.GetActiveScene().name != this.sceneName) {
            this.isActive = false;
            // TODO: Use color from pallete
            this.background.color = new Color(0.4118f, 0f, 0.5882f); // dark violet
        }
    }
}
