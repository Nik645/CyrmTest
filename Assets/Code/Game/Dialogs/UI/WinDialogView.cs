using UnityEngine;
using UnityEngine.UI;

public class WinDialogView : DialogView<IWinDialogModel>
{
    [SerializeField] private Button _endGameButton;

    private IWinDialogModel _data;
    
    protected override void SetData(IWinDialogModel data)
    {
        _data = data;
    }
    
    protected override void Start()
    {
        base.Start();
        _endGameButton.onClick.AddListener(HandleEndGameButtonOnClick);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _endGameButton.onClick.RemoveListener(HandleEndGameButtonOnClick);
    }

    private void HandleEndGameButtonOnClick()
    {
        _data.EndGame?.Invoke();
    }
}