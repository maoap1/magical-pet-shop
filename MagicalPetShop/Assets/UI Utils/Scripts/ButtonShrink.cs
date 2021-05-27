using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonShrink : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public GameObject target = null;

    public void Start()
    {
        if (!target) target = gameObject;
    }

    public void OnPointerDown(PointerEventData eventData) {
        Button button = GetComponent<Button>();
        if (button == null || (button != null && button.interactable)) {
            target.transform.DOScale(new Vector3(0.95f, 0.95f, 0.95f), 0.1f);
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (target != null)
            target.transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f);
    }
}
