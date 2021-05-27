using UnityEngine;
using UnityEngine.Events;

public class Tweenable : MonoBehaviour
{
    public UnityEvent OnEnableTween = null;
    public UnityEvent OnDisableTween = null;

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
        if (IsNull(OnDisableTween)) gameObject.SetActive(false);
        else OnDisableTween.Invoke();
    }

    public void Enable()
    {
        if (IsNull(OnEnableTween)) gameObject.SetActive(true);
        else OnEnableTween.Invoke();
    }
}
