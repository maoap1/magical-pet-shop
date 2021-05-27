using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonAlpha : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public GameObject target = null;
    public float targetAlpha = 0.8f;
    private float originalAlpha;

    public void Start() {
        if (!target) target = gameObject;
        originalAlpha = target.GetComponent<Image>().color.a;
    }

    public void OnPointerDown(PointerEventData eventData) {
        Button button = GetComponent<Button>();
        if (button == null || (button != null && button.interactable)) {
            target.GetComponent<Image>().DOFade(targetAlpha, 0.1f);
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (target != null)
            target.GetComponent<Image>().DOFade(originalAlpha, 0.1f);
    }
}
