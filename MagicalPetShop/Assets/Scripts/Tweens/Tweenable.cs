using UnityEngine;
using UnityEngine.Events;

public class Tweenable : MonoBehaviour
{
    public UnityEvent OnDisableTween = null;
    public UnityEvent OnEnableTween = null;

    public void Disable()
    {
        if (OnDisableTween == null) gameObject.SetActive(false);
        else OnDisableTween.Invoke();
    }

    public void Enable()
    {
        if (OnEnableTween == null) gameObject.SetActive(true);
        else OnEnableTween.Invoke();
    }
}
