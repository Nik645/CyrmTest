using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DialogView : MonoBehaviour
{
    [SerializeField] private Button _closeButton;

    private readonly List<IDisposable> _disposeOnDestroy = new List<IDisposable>();
    
    public event Action OnClose;
    public abstract void SetData(IDialogModel data);

    protected virtual void Start()
    {
        _closeButton.onClick.AddListener(InvokeOnClose);
    }

    protected virtual void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(InvokeOnClose);
        OnClose = null;
        foreach (var disposable in _disposeOnDestroy)
        {
            disposable.Dispose();
        }
        _disposeOnDestroy.Clear();
    }

    protected void InvokeOnClose()
    {
        OnClose?.Invoke();
    }

    protected void AddDisposeOnDestory(IDisposable disposable)
    {
        _disposeOnDestroy.Add(disposable);
    }
}

public abstract class DialogView<T> : DialogView where T : IDialogModel
{
    public override void SetData(IDialogModel data)
    {
        if (data is IDisposable disposable)
        {
            AddDisposeOnDestory(disposable);
        }
        
        SetData((T) data);
    }

    protected abstract void SetData(T data);
}