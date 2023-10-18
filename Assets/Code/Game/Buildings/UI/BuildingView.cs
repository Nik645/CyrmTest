using System;
using TMPro;
using UnityEngine;

public class BuildingView : MonoBehaviour
{
    [SerializeField] private Clicker _clicker;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private TMP_Text _name;
    
    private Action _onClick;
    
    public void SetData(Sprite sprite, string name, Action onClick)
    {
        _sprite.sprite = sprite;
        _name.text = name;
        _onClick = onClick;
    }
    
    private void Start()
    {
        _clicker.OnClick += HandleOnClick;
    }

    private void OnDestroy()
    {
        _clicker.OnClick -= HandleOnClick;
    }

    private void HandleOnClick()
    {
        _onClick?.Invoke();
    }
}
