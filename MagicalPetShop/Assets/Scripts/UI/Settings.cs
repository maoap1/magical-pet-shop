using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField]
    GameObject content;

    public void Open() {
        AppearHideComponent ahc = GetComponent<AppearHideComponent>();
        foreach (GameObject go in ahc.ObjectsToAppear) {
            go.TweenAwareEnable();
        }
        foreach (GameObject go in ahc.ObjectsToHide) {
            go.TweenAwareDisable();
        }
        content.SetActive(true);
    }

    public void Close() {
        AppearHideComponent ahc = GetComponent<AppearHideComponent>();
        foreach (GameObject go in ahc.ObjectsToAppear) {
            go.TweenAwareDisable();
        }
        foreach (GameObject go in ahc.ObjectsToHide) {
            go.TweenAwareEnable();
        }
        content.SetActive(false);
    }

}
