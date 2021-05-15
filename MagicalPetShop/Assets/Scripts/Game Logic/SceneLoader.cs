using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public string sceneName;

    public void LoadSceneOfName(string sceneName) {
        if (transitionSounds.ContainsKey(this.sceneName)) {
            FindObjectOfType<AudioManager>().Play(transitionSounds[this.sceneName]);
        }
        SceneManager.LoadScene(sceneName);
    }

    Dictionary<string, SoundType> transitionSounds = new Dictionary<string, SoundType>() {
        { "Lab", SoundType.Steps },
        { "Shop", SoundType.CustomerAppear },
        { "Yard", SoundType.Door }
    };

}
