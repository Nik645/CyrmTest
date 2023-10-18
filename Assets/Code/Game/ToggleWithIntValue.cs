using System;
using UnityEngine;
using UnityEngine.UI;

public class ToggleWithIntValue : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private int _value;

    private Action<int> _actions;
    
    public void Subscribe(Action<int> action)
    {
        _actions += action;
        if (_toggle.isOn)
        {
            action.Invoke(_value);
        }
    }

    public void Unsubscribe(Action<int> action)
    {
        _actions -= action;
    }

    private void Start()
    {
        _toggle.onValueChanged.AddListener(HandleToggleOnValueChanged);
    }

    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(HandleToggleOnValueChanged);
        _actions = null;
    }

    private void HandleToggleOnValueChanged(bool isOn)
    {
        if (isOn)
        {
            _actions?.Invoke(_value);
        }
    }
}