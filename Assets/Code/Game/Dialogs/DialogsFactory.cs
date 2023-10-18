using UnityEngine;

public class DialogsFactory : IFactory<DialogView, IDialogModel>
{
    private readonly DialogsSettings _dialogsSettings;

    public DialogsFactory(DialogsSettings dialogsSettings)
    {
        _dialogsSettings = dialogsSettings;
    }

    public DialogView Create(IDialogModel arg)
    {
        var prefab = _dialogsSettings.GetDialogByType((DialogsType)arg.Type);
        if (prefab == null)
        {
            return null;
        }

        var dialog = Object.Instantiate(prefab);
        dialog.SetData(arg);
        return dialog;
    }
}