using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperPanelCanvas : MonoBehaviour
{
    void Awake() {
        UpperPanelCanvas[] objs = GameObject.FindObjectsOfType<UpperPanelCanvas>();
        if (objs.Length > 1) {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
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
