using UnityEngine;

public class SafeArea : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    
    private void Awake()
    {
        RefreshBounds();
    }

    private void RefreshBounds()
    {
        float w = Screen.width;
        float h = Screen.height;

        Rect safe = Screen.safeArea;

        if (Screen.width > 0 && Screen.height > 0)
        {
            Vector2 anchorMin = safe.position;
            Vector2 anchorMax = safe.position + safe.size;
            anchorMin.x /= w;
            anchorMin.y /= h;
            anchorMax.x /= w;
            anchorMax.y /= h;

            if (anchorMin.x >= 0 && anchorMin.y >= 0 && anchorMax.x >= 0 && anchorMax.y >= 0)
            {
                _rectTransform.anchorMin = anchorMin;
                _rectTransform.anchorMax = anchorMax;
            }
        }
    }
}
