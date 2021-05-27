using UnityEngine;

/// <summary>
/// This is a dirty bug fix of portals being rendered on top of lights
/// Yes, this strange complexity is needed =D
/// </summary>

public class EnableOnStartChildren : MonoBehaviour
{
    void Start()
    {
        
        Invoke(nameof(TryIt), 0.01f);
    }

    [ContextMenu(nameof(TryIt))]
    void TryIt()
    {
        DisableAll();
        Invoke(nameof(EnableAll), 0.01f);
    }



    [ContextMenu(nameof(DisableAll))]
    void DisableAll()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    [ContextMenu(nameof(EnableAll))]
    void EnableAll()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }


}
