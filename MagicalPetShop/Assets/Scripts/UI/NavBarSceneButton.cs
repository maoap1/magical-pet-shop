using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NavBarSceneButton : MonoBehaviour {

    public string sceneName;

    private bool isActive = false;

    private Image image;

    Dictionary<string, SoundType> transitionSounds = new Dictionary<string, SoundType>() {
        { "Lab", SoundType.Steps },
        { "Shop", SoundType.CustomerAppear },
        { "Yard", SoundType.Door }
    };

    public void LoadScene() {
        if (transitionSounds.ContainsKey(this.sceneName)) {
            FindObjectOfType<AudioManager>().Play(transitionSounds[this.sceneName]);
        }
        SceneManager.LoadScene(sceneName);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the current scene is scene of this button, 
        if (!this.isActive && SceneManager.GetActiveScene().name == this.sceneName) {
            this.isActive = true;
            // TODO: Use color from pallete
            this.image.color = new Color(255, 215, 0); // gold
        }
        // if the button is active and this is not its scene, deactivate it
        if (this.isActive && SceneManager.GetActiveScene().name != this.sceneName) {
            this.isActive = false;
            // TODO: Use color from pallete
            this.image.color = new Color(176, 0, 180); // violet
        }
    }
}
