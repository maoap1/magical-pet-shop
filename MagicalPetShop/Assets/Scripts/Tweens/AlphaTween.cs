using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class AlphaTween : MonoBehaviour
{
    public float duration;
    public float backDuration;
    public bool disable = true;
    public bool enable = true;
    public bool destroy = false;

    public void FadeIn()
    {
        var cg = GetComponent<CanvasGroup>();
        cg.alpha = 0f;
        if (enable) gameObject.SetActive(true);
        cg.DOFade(1f, duration);
    }

    public void FadeOut()
    {
        var cg = GetComponent<CanvasGroup>();
        cg.alpha = 1f;
        cg.DOFade(0f, backDuration).OnComplete(() => 
            {
                if (disable) gameObject.SetActive(false);
                if (destroy) Invoke(nameof(DestroyThis), 0.5f);
            });
    }

    private void DestroyThis()
    {
        GameObject.Destroy(gameObject);
    }

}
