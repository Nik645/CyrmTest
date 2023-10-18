using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IRecycleDialogModel : IDialogModel
{
    public IRecycleCrafter Crafter { get; }
    public IReadOnlyReactiveProperty<Sprite> ToolIcon { get; }
    public IEnumerable<IItemSwitchViewModel> Resources { get; }
}

public class RecycleDialogModel : IRecycleDialogModel, IDisposable
{
    public DialogsType Type => DialogsType.Recycle;

    public IRecycleCrafter Crafter { get; }
    public IReadOnlyReactiveProperty<Sprite> ToolIcon => _toolIcon;
    public IEnumerable<IItemSwitchViewModel> Resources => _resources;
    private readonly List<ItemSwitchViewModel> _resources = new List<ItemSwitchViewModel>();
    private readonly ReactiveProperty<Sprite> _toolIcon = new ReactiveProperty<Sprite>();

    private ICommandDispatcher _commandDispatcher;

    public RecycleDialogModel(IRecycleCrafter crafter, ICommandDispatcher commandDispatcher)
    {
        Crafter = crafter;
        _commandDispatcher = commandDispatcher;
        _resources.AddRange(crafter.Resources.Select(x => new ItemSwitchViewModel(x, commandDispatcher)));
        Crafter.Tool.Subscribe(SetToolIcon);
    }

    public void Dispose()
    {
        Crafter.Tool.Unsubscribe(SetToolIcon);
        _toolIcon.Dispose();
        foreach (var resourceViewModel in _resources)
        {
            resourceViewModel.Dispose();
        }

        _resources.Clear();
    }

    private void SetToolIcon(ItemType item)
    {
        _toolIcon.Value = _commandDispatcher.Execute(new GetItemIconQuery(item));
    }
}