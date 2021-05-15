using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwipeDetection : MonoBehaviour
{
    public int ThresholdPx;

    public UnityEvent OnSwipeLeft;
    public UnityEvent OnSwipeRight;
    public UnityEvent OnSwipeUp;
    public UnityEvent OnSwipeDown;

    private Vector2 startPos;
    private bool fingerDown;


    private bool HasTouchStarted()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began;
#else
        return Input.GetMouseButtonDown(0);
#endif
    }

    private Vector2 GetTouchPosition()
    {

#if UNITY_ANDROID && !UNITY_EDITOR
        return Input.touches[0].position;
#else
        return Input.mousePosition;
#endif

    }

    private bool HasTouchEnded()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended;
#else
        return Input.GetMouseButtonUp(0);
#endif

    }




    private void CheckForSwipe()
    {
        if (!fingerDown && HasTouchStarted())
        {
            startPos = GetTouchPosition();
            fingerDown = true;
        }

        if (fingerDown)
        {
            // Up
            if (GetTouchPosition().y >= startPos.y + ThresholdPx)
            {
                fingerDown = false;
                OnSwipeUp.Invoke();
            }
            // Down
            if (GetTouchPosition().y <= startPos.y - ThresholdPx)
            {
                fingerDown = false;
                OnSwipeDown.Invoke();
            }
            // Right
            if (GetTouchPosition().x >= startPos.x + ThresholdPx)
            {
                fingerDown = false;
                OnSwipeRight.Invoke();
            }
            // Left
            if (GetTouchPosition().x <= startPos.x - ThresholdPx)
            {
                fingerDown = false;
                OnSwipeLeft.Invoke();
            }
        }

        if(fingerDown && HasTouchEnded())
        {
            fingerDown = false;
        }
    }

    private void Update()
    {
        CheckForSwipe();
    }
}
