using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscExit : MonoBehaviour
{
    private void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            PlayerState.THIS.Save();
            Application.Quit();
        }

    }
}
