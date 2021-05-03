using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NavButton : MonoBehaviour
{
    Dictionary<string, AudioType> transitionSounds = new Dictionary<string, AudioType>() {
        { "Lab", AudioType.Steps },
        { "Shop", AudioType.CustomerAppear },
        { "Yard", AudioType.Door }
    };

    public void LoadScene(string sceneName) {
        if (transitionSounds.ContainsKey(sceneName)) {
            FindObjectOfType<AudioManager>().Play(transitionSounds[sceneName]);
        }
        SceneManager.LoadScene(sceneName);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
