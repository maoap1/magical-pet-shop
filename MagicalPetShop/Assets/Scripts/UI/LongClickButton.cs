using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LongClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    [SerializeField]
    float requiredDuration;

    public UnityEvent onClick;
    public UnityEvent onLongClick;


    bool pressed;
    float pressedDuration;

    public void OnPointerDown(PointerEventData eventData) {
        this.pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (this.pressed) {
            FindObjectOfType<AudioManager>().Play(SoundType.TabSwitch);
            this.onClick.Invoke();
        }
        this.pressed = false;
        this.pressedDuration = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.pressed) {
            this.pressedDuration += Time.deltaTime;
            if (this.pressedDuration >= this.requiredDuration) {
                FindObjectOfType<AudioManager>().Play(SoundType.Click);
                this.onLongClick.Invoke();
                this.pressed = false;
                this.pressedDuration = 0;
            }
        }
    }
}
