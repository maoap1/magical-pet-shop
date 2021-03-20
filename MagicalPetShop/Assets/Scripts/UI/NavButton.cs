using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NavButton : MonoBehaviour
{

    private string scene = "shop";

    public void SetScene(string sceneName) {
        this.scene = sceneName;
    }

    public void LoadScene() {
        SceneManager.LoadScene(this.scene);
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
