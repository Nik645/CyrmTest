using System;

public interface IResourceCrafter : ICrafter
{
    IItemSwitchReactiveProperty Resource { get; }
}

public class ResourceCrafter : IResourceCrafter, ITick, IDisposable
{
    public IItemSwitchReactiveProperty Resource => _resource;
    private readonly ResourceReactiveProperty _resource;
    
    public IReadOnlyReactiveProperty<bool> Active => _active;
    private readonly ReactiveProperty<bool> _active = new ReactiveProperty<bool>();
    
    private readonly ResourceCraftSettings _resourceCraftSettings;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ITickService _craftService;

    private DateTime _startTime;

    public ResourceCrafter(ResourceCraftSettings resourceCraftSettings,
        ICommandDispatcher commandDispatcher,
        ITickService craftService)
    {
        _resourceCraftSettings = resourceCraftSettings;
        _commandDispatcher = commandDispatcher;
        _craftService = craftService;
        _resource = new ResourceReactiveProperty();
        Resource.Subscribe(HandleResourceOnChange);
    }
    
    public void Tick()
    {
        if (!Active.Value)
        {
            return;
        }
        
        if ((DateTime.Now - _startTime).TotalSeconds > _resourceCraftSettings.CraftTime)
        {
            _commandDispatcher.Execute(new GiveItemCommand(Resource.Value, _resourceCraftSettings.CraftAmount));
            SetStartTime();
        }
    }

    public void Start()
    {
        SetStartTime();
        _active.Value = _resource.IsValid();
    }

    public void Stop()
    {
        _active.Value = false;
    }

    public void Dispose()
    {
        _active.Value = false;
        _active.Dispose();
        _resource.Dispose();
        _craftService.Remove(this);
    }

    private void HandleResourceOnChange(ItemType _)
    {
        _active.Value = false;
    }
    
    private void SetStartTime()
    {
        _startTime = DateTime.Now;
    }
}