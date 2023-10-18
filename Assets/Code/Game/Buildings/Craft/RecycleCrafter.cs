using System;
using System.Collections.Generic;
using System.Linq;

public interface IRecycleCrafter : ICrafter
{
    IReadOnlyList<IItemSwitchReactiveProperty> Resources { get; }
    IReadOnlyReactiveProperty<ItemType> Tool { get; }
}

public class RecycleCrafter : IRecycleCrafter, ITick, IDisposable
{
    public IReadOnlyReactiveProperty<ItemType> Tool => _tool;
    private ReactiveProperty<ItemType> _tool = new ReactiveProperty<ItemType>();

    public IReadOnlyList<IItemSwitchReactiveProperty> Resources => _resources;
    private List<ResourceReactiveProperty> _resources = new List<ResourceReactiveProperty>();
    
    public IReadOnlyReactiveProperty<bool> Active => _active;
    private readonly ReactiveProperty<bool> _active = new ReactiveProperty<bool>();
    
    private readonly RecycleCraftSettings _recycleCraftSettings;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ITickService _craftService;

    private DateTime _startTime;
    private RecipeTool _recipeTool;

    public RecycleCrafter(RecycleCraftSettings recycleCraftSettings, 
        ICommandDispatcher commandDispatcher,
        ITickService craftService)
    {
        _recycleCraftSettings = recycleCraftSettings;
        _commandDispatcher = commandDispatcher;
        _craftService = craftService;
        for (int i = 0; i < _recycleCraftSettings.ResourcesCount; i++)
        {
            var resource = new ResourceReactiveProperty();
            resource.Subscribe(HandleResourceOnChange);
            _resources.Add(resource);
        }
    }

    public void Tick()
    {
        if (!_active.Value || _recipeTool == null)
        {
            return;
        }
        
        if ((DateTime.Now - _startTime).TotalSeconds > _recycleCraftSettings.CraftTime)
        {
            foreach (var resource in _recipeTool.Resources)
            {
                _commandDispatcher.Execute(new TakeItemCommand(resource.Item, resource.Amount));
            }
            
            _commandDispatcher.Execute(new GiveItemCommand(_recipeTool.Tool.Item, _recipeTool.Tool.Amount));
            SetStartTime();
            _active.Value = IsCraftAvailable();
        }
    }

    public void Start()
    {
        SetStartTime();
        SetRecipe();
        _active.Value = IsCraftAvailable();
    }

    public void Stop()
    {
        _active.Value = false;
    }

    public void Dispose()
    {
        _active.Value = false;
        _active.Dispose();
        foreach (var resource in _resources)
        {
            resource.Dispose();
        }
        _tool.Dispose();
        _craftService.Remove(this);
    }

    private void HandleResourceOnChange(ItemType _)
    {
        _active.Value = false;
        SetRecipe();
    }
    
    private void SetStartTime()
    {
        _startTime = DateTime.Now;
    }

    private void SetRecipe()
    {
        _recipeTool = _recycleCraftSettings.Recipes.FirstOrDefault(x => x.Resources.All(item =>
            _resources.Any(resource => resource.Value == item.Item)));
        _tool.Value = _recipeTool?.Tool.Item ?? ItemType.None;
    }

    private bool IsCraftAvailable()
    {
        if (_recipeTool == null)
            return false;

        foreach (var resource in _recipeTool.Resources)
        {
            var amount = _commandDispatcher.Execute(new GetItemAmountQuery(resource.Item));
            if (amount < resource.Amount)
            {
                return false;
            }
        }
        
        return true;
    }
}