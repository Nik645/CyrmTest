using System.Collections.Generic;
using Code.Dialogs;
using UnityEngine;
using UnityEngine.UI;

public class RecycleDialogView : DialogView<IRecycleDialogModel>
{
    [SerializeField] private ItemSwitchView _prefab;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _stopButton;
    [SerializeField] private Image _toolIcon;
    [SerializeField] private Transform _resourcesHolder;

    private readonly List<ItemSwitchView> _resources = new List<ItemSwitchView>();
    
    private IRecycleCrafter _recycleCrafter;
    private IRecycleDialogModel _data;
    
    protected override void SetData(IRecycleDialogModel data)
    {
        _recycleCrafter = data.Crafter;
        _data = data;
        
        foreach (var resource in data.Resources)
        {
            var view = Instantiate(_prefab, _resourcesHolder, false);
            view.SetData(resource);
            _resources.Add(view);
        }
    }
    
    protected override void Start()
    {
        base.Start();
        _startButton.onClick.AddListener(HandleStartCraftOnClick);
        _stopButton.onClick.AddListener(HandleStopCraftOnClick);
        _recycleCrafter.Active.Subscribe(SetButtonsState);
        _data.ToolIcon.Subscribe(HandleToolIconOnChange);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        
        foreach (var resourceView in _resources)
        {
            Destroy(resourceView.gameObject);
        }
        
        _resources.Clear();
        
        _startButton.onClick.RemoveListener(HandleStartCraftOnClick);
        _stopButton.onClick.RemoveListener(HandleStopCraftOnClick);
        _recycleCrafter.Active.Unsubscribe(SetButtonsState);
        _data.ToolIcon.Unsubscribe(HandleToolIconOnChange);
    }
    
    private void SetButtonsState(bool craftActive)
    {
        _startButton.gameObject.SetActive(!craftActive);
        _stopButton.gameObject.SetActive(craftActive);
    }
    
    private void HandleStartCraftOnClick()
    {
        _recycleCrafter.Start();
    }

    private void HandleStopCraftOnClick()
    {
        _recycleCrafter.Stop();
    }

    private void HandleToolIconOnChange(Sprite sprite)
    {
        _toolIcon.sprite = sprite;
    }
}