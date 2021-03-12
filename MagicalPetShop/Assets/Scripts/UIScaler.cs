using System;
using UnityEngine;

[ExecuteInEditMode]
public class UIScaler : MonoBehaviour
{
    
    public int widthUnits = 9;
    public int heightUnits = 16;

    private float _lastWidth = 0f;
    private float _lastHeight = 0f;

    void Update()
    {
        var parentRect = transform.parent.GetComponent<RectTransform>();
        float width = parentRect.rect.width;
        float height = parentRect.rect.height;
        if ((Mathf.Abs(width - _lastWidth) > 0.1) ||
            (Mathf.Abs(height - _lastHeight) > 0.1))
        {
            Recompute(width, height);
        }
    }

    private void Recompute(float width, float height)
    {
        var aspectHeight = width / widthUnits * heightUnits;
        if (Mathf.Abs(aspectHeight - height) < 1f)
        {
            SetSize(width, height);
        }
        if (aspectHeight < height)
        {
            SetSize(width, aspectHeight);
        }
        else
        {
            var aspectWidth = height / heightUnits * widthUnits;
            SetSize(aspectWidth, height);
        }
    }

    private void SetSize(float width, float height)
    {
        var rect = GetComponent<RectTransform>();
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }
}
