using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

public class LongClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    [SerializeField]
    float requiredDuration;

    [SerializeField]
    GameObject longPressIndication;
    [SerializeField]
    ProgressBar progressBar;

    [SerializeField]
    float startOfIndication;

    public UnityEvent onClick;
    public UnityEvent onLongClick;


    bool pressed;
    float pressedDuration;

    public void OnPointerDown(PointerEventData eventData) {
        this.pressed = true;
        gameObject.transform.DOScale(new Vector3(0.95f, 0.95f, 0.95f), 0.1f);
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (this.pressed) {
            FindObjectOfType<AudioManager>().Play(SoundType.TabSwitch);
            this.onClick.Invoke();
        }
        this.pressed = false;
        this.pressedDuration = 0;
        this.longPressIndication.SetActive(false);
        this.progressBar.fillRate = 0;
        gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f);
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
                this.longPressIndication.SetActive(false);
                this.progressBar.fillRate = 0;
                gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f);
            } else if (this.pressedDuration >= this.startOfIndication) {
                this.progressBar.fillRate = (this.pressedDuration - this.startOfIndication) / (this.requiredDuration - this.startOfIndication);
                this.longPressIndication.SetActive(true);
            }
        }
    }
}
