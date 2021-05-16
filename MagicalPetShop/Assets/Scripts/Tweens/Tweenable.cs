using UnityEngine;
using UnityEngine.Events;

public class Tweenable : MonoBehaviour
{
    public UnityEvent OnDisableTween = null;
    public UnityEvent OnEnableTween = null;

    private bool IsNull(UnityEvent unityEvent)
    {
        if (unityEvent == null) return true;
        for (int i = 0; i < unityEvent.GetPersistentEventCount(); i++)
        {
            if (unityEvent.GetPersistentTarget(i) != null)
            {
                return false;
            }
        }
        return true;
    }

    public void Disable()
    {
        if (IsNull(OnDisableTween)) { gameObject.SetActive(false); Debug.Log("Disable null"); }
        else { OnDisableTween.Invoke(); Debug.Log("Disable tween"); }
    }

    public void Enable()
    {
        if (IsNull(OnEnableTween)) { gameObject.SetActive(true); Debug.Log("Enable null"); }
        else { OnEnableTween.Invoke(); Debug.Log("Enable tween"); }
    }
}
