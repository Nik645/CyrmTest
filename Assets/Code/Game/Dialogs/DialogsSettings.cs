using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogSettings", menuName = "SO/DialogSettings")]
public class DialogsSettings : ScriptableObject
{
    [Serializable]
    public class Data
    {
        [SerializeField] private DialogsType _type;
        [SerializeField] private DialogView _prefab;

        public DialogsType Type => _type;
        public DialogView Prefab => _prefab;
    }

    [SerializeField] private List<Data> _dialogs = new List<Data>();

    public DialogView GetDialogByType(DialogsType type)
    {
        return _dialogs.FirstOrDefault(x => x.Type == type)?.Prefab;
    }
}
