using System;
using UnityEngine;

public interface IMarketDialogModel : IDialogModel
{
    IItemSwitchViewModel ItemModel { get; }
    IReadOnlyReactiveProperty<string> Price { get; }
    IReadOnlyReactiveProperty<Sprite> CurrencyIcon { get; }
    void SellItem();
}

public class MarketDialogModel : IMarketDialogModel, IDisposable
{
    public DialogsType Type => DialogsType.Market;
    public IItemSwitchViewModel ItemModel => _itemModel;
    public IReadOnlyReactiveProperty<string> Price => _price;
    public IReadOnlyReactiveProperty<Sprite> CurrencyIcon => _currencyIcon;

    private readonly ItemSwitchViewModel _itemModel;
    private readonly InventoryItemReactiveProperty _inventoryItem;
    private readonly ReactiveProperty<string> _price = new ReactiveProperty<string>();
    private readonly ReactiveProperty<Sprite> _currencyIcon = new ReactiveProperty<Sprite>();

    private readonly ICommandDispatcher _commandDispatcher;
    
    public MarketDialogModel(ICommandDispatcher commandDispatcher, IInventory inventory)
    {
        _commandDispatcher = commandDispatcher;
        _inventoryItem = new InventoryItemReactiveProperty(inventory);
        _itemModel = new ItemSwitchViewModel(_inventoryItem, commandDispatcher);
        _inventoryItem.Subscribe(HandleItemOnChange);
    }

    public void Dispose()
    {
        _itemModel.Dispose();
        _inventoryItem.Dispose();
        _price.Dispose();
        _currencyIcon.Dispose();
        _inventoryItem.Unsubscribe(HandleItemOnChange);
    }

    public void SellItem()
    {
        _commandDispatcher.Execute(new SellItemCommand(_inventoryItem.Value, 1));
    }

    private void HandleItemOnChange(ItemType itemType)
    {
        var price = _commandDispatcher.Execute(new GetItemPriceQuery(itemType));
        if (price == null)
        {
            _price.Value = "";
            _currencyIcon.Value = null;
            return;
        }

        _price.Value = price.Price.ToString();
        _currencyIcon.Value = _commandDispatcher.Execute(new GetCurrencyIconQuery(price.Currency));
    }
}