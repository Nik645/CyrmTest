using System;
using UnityEngine;

public class ItemSwitchViewModel : IItemSwitchViewModel, IDisposable
{
    public IReadOnlyReactiveProperty<Sprite> Icon => _resourceIcon;
    private readonly ReactiveProperty<Sprite> _resourceIcon = new ReactiveProperty<Sprite>();

    private readonly IItemSwitchReactiveProperty _resource;
    private readonly ICommandDispatcher _commandDispatcher;

    public ItemSwitchViewModel(IItemSwitchReactiveProperty resource, ICommandDispatcher commandDispatcher)
    {
        _resource = resource;
        _commandDispatcher = commandDispatcher;
        _resource.Subscribe(SetIcon);
    }

    public void SwitchItem()
    {
        _resource.SwitchItem();
    }

    public void Dispose()
    {
        _resource.Unsubscribe(SetIcon);
        _resourceIcon.Dispose();
    }

    private void SetIcon(ItemType itemType)
    {
        _resourceIcon.Value = _commandDispatcher.Execute(new GetItemIconQuery(itemType));
    }
}