using UnityEngine;

public class DialogsController : MonoBehaviour
{
    [SerializeField] private Transform _dialogsParent;
    
    private IDialogsHolder _dialogsHolder;
    private IFactory<DialogView, IDialogModel> _factory;

    private DialogView _currentDialog;
    
    public void Init(IDialogsHolder dialogsHolder, IFactory<DialogView, IDialogModel> factory)
    {
        _dialogsHolder = dialogsHolder;
        _factory = factory;
    }

    private void Start()
    {
        _dialogsHolder.OnChange += HandleOnChange;
        CreateDialog(_dialogsHolder.Current);
    }

    private void OnDestroy()
    {
        _dialogsHolder.OnChange -= HandleOnChange;
        DestroyCurrentDialog();
    }

    private void CreateDialog(IDialogModel data)
    {
        DestroyCurrentDialog();

        if (data == null)
        {
            return;
        }

        var dialog = _factory.Create(data);
        if (dialog != null)
        {
            dialog.transform.SetParent(_dialogsParent, false);
            _currentDialog = dialog;
            _currentDialog.OnClose += HandleOnClose;
        }
    }

    private void DestroyCurrentDialog()
    {
        if (_currentDialog == null)
        {
            return;
        }
        
        _currentDialog.OnClose -= HandleOnClose;
        Destroy(_currentDialog.gameObject);
        _currentDialog = null;
    }
    
    private void HandleOnChange(IDialogModel data)
    {
        CreateDialog(data);
    }

    private void HandleOnClose()
    {
        DestroyCurrentDialog();
        _dialogsHolder.Remove(_dialogsHolder.Current);
    }
}