using System;
using System.Collections.Generic;

public class DialogsHolder : IDialogsHolder
{
    private readonly List<IDialogModel> _datas = new List<IDialogModel>();

    public event Action<IDialogModel> OnChange;
    public IDialogModel Current { get; private set; }

    public void Add(IDialogModel data)
    {
        _datas.Add(data);
        SetCurrent();
    }

    public void Remove(IDialogModel data)
    {
        _datas.Remove(data);
        SetCurrent();
    }

    private void SetCurrent()
    {
        var newCurrent =  _datas.Count > 0 ? _datas[0] : null;

        if (Current != newCurrent)
        {
            Current = newCurrent;
            OnChange?.Invoke(Current);
        }
    }
}
