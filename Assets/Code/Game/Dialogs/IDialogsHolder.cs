using System;

public interface IDialogsHolder
{
    event Action<IDialogModel> OnChange;
    IDialogModel Current { get; }
    void Add(IDialogModel data);
    void Remove(IDialogModel data);
}