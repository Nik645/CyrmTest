using System.Collections.Generic;
using System.Linq;

public class InventoryItemReactiveProperty : ReactiveProperty<ItemType>, IItemSwitchReactiveProperty
{
    private readonly IInventory _inventory;

    public InventoryItemReactiveProperty(IInventory inventory)
    {
        _inventory = inventory;
        _inventory.OnChange += HandleInventoryOnChange;
    }
    
    public void SwitchItem()
    {
        var items = GetInventoryItems();
        if (items.Count == 0)
        {
            Value = ItemType.None;
            return;
        }

        Value = items.GetNextItem(Value);
    }

    public bool IsValid()
    {
        return GetInventoryItems().Contains(Value);
    }

    public override void Dispose()
    {
        base.Dispose();
        _inventory.OnChange -= HandleInventoryOnChange;
    }
    
    private void HandleInventoryOnChange()
    {
        if (Value == ItemType.None)
        {
            return;
        }
        
        var items = GetInventoryItems();
        if (items.IndexOf(Value) < 0)
        {
            Value = ItemType.None;
        }
    }

    private List<ItemType> GetInventoryItems()
    {
        return _inventory.Items.
            Where(x => x.Value > 0).
            OrderBy(x => x.Key).
            Select(x => x.Key).
            ToList();
    }
}