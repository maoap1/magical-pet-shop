using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonShrink : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public void OnPointerDown(PointerEventData eventData) {
        Button button = GetComponent<Button>();
        if (button == null || (button != null && button.interactable)) {
            gameObject.transform.DOScale(new Vector3(0.95f, 0.95f, 0.95f), 0.1f);
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (gameObject != null)
            gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f);
    }
}
