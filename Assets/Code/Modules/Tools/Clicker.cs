using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clicker : MonoBehaviour, IPointerClickHandler
{
    public event Action OnClick;
    
    // private void OnMouseUp()
    // {
    //     OnClick?.Invoke();
    // }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }
}
