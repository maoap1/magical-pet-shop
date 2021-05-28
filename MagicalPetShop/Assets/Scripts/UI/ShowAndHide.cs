using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAndHide : MonoBehaviour
{
    public float seconds = 2;

    private bool running;

    public void Execute() {
        if (!running) {
            running = true;
            GetComponent<Tweenable>().Enable();
            Invoke(nameof(Hide), seconds);
        }
    }

    private void Hide() {
        GetComponent<Tweenable>().Disable();
        running = false;
    }
}
