using Code.Dialogs;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDialogView : DialogView<IResourceDialogModel>
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _stopButton;
    [SerializeField] private ItemSwitchView _resourceView;
    
    private IResourceCrafter _crafter;

    protected override void SetData(IResourceDialogModel data)
    {
        _crafter = data.Crafter;
        _resourceView.SetData(data.ResourceModel);
    }

    protected override void Start()
    {
        base.Start();
        _startButton.onClick.AddListener(HandleStartCraftOnClick);
        _stopButton.onClick.AddListener(HandleStopCraftOnClick);
        _crafter.Active.Subscribe(SetButtonsState);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _startButton.onClick.RemoveListener(HandleStartCraftOnClick);
        _stopButton.onClick.RemoveListener(HandleStopCraftOnClick);
        _crafter.Active.Unsubscribe(SetButtonsState);
    }
    
    private void SetButtonsState(bool craftActive)
    {
        _startButton.gameObject.SetActive(!craftActive);
        _stopButton.gameObject.SetActive(craftActive);
    }
    
    private void HandleStartCraftOnClick()
    {
        _crafter.Start();
    }

    private void HandleStopCraftOnClick()
    {
        _crafter.Stop();
    }
}