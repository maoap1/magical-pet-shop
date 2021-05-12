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
        this.image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isActive && SceneManager.GetActiveScene().name == this.sceneName) {
            this.isActive = true;
            // TODO: Use color from pallete
            this.image.color = new Color(0.651f, 0.486f, 0f); // dark gold
        }
        if (this.isActive && SceneManager.GetActiveScene().name != this.sceneName) {
            this.isActive = false;
            // TODO: Use color from pallete
            this.image.color = new Color(0.4118f, 0f, 0.5882f); // dark violet
        }
    }
}
