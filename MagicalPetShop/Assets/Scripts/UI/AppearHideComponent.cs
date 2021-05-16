using System.Collections.Generic;
using UnityEngine;

public class AppearHideComponent : MonoBehaviour
{
    public List<GameObject> ObjectsToAppear;
    
    public List<GameObject> ObjectsToHide;

    public void Do()
    {
        gameObject.TweenAwareEnable();
        foreach (GameObject go in ObjectsToAppear)
        {
            go.TweenAwareEnable();
        }
        foreach (GameObject go in ObjectsToHide)
        {
            go.TweenAwareDisable();
        }
    }

    public void Revert()
    {
        gameObject.TweenAwareDisable();
        foreach (GameObject go in ObjectsToAppear)
        {
            go.TweenAwareDisable();
        }
        foreach (GameObject go in ObjectsToHide)
        {
            go.TweenAwareEnable();
        }
    }
}
