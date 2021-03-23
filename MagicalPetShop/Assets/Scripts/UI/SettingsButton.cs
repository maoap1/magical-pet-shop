using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{

    private GameObject settings;

    public void SetSettings(GameObject settings) {
        this.settings = settings;
    }

    public void ShowSettings() {
        this.settings.SetActive(true);
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
