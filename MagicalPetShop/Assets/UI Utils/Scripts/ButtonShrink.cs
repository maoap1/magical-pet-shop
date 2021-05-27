using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonShrink : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public GameObject target = null;
    public float targetScalePercent = 0.95f;
    private float originalScaleX;
    private float originalScaleY;

    public void Start()
    {
        if (!target) target = gameObject;
        originalScaleX = target.transform.localScale.x;
        originalScaleY = target.transform.localScale.y;
    }

    public void OnPointerDown(PointerEventData eventData) {
        Button button = GetComponent<Button>();
        if (button == null || (button != null && button.interactable)) {
            target.transform.DOScale(new Vector3(targetScalePercent * originalScaleX, targetScalePercent * originalScaleY, 1f), 0.1f);
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (target != null)
            target.transform.DOScale(new Vector3(originalScaleX, originalScaleY, 1f), 0.1f);
    }
}
