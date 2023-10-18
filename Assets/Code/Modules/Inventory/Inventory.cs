using System;
using System.Collections.Generic;

public interface IInventory
{
    event Action OnChange;
    IReadOnlyDictionary<ItemType, int> Items { get; }
    void GiveItems(ItemType type, int amount);
    bool TakeItems(ItemType type, int amount);
    int ItemsAmount(ItemType type);
}

public class Inventory : IInventory
{
    public event Action OnChange;
    public IReadOnlyDictionary<ItemType, int> Items => _saveData.Data;
    private readonly ISaveData<Dictionary<ItemType, int>> _saveData;

    public Inventory(ISaveData<Dictionary<ItemType, int>> saveData)
    {
        _saveData = saveData;
    }

    public void GiveItems(ItemType type, int amount)
    {
        if (!_saveData.Data.ContainsKey(type))
        {
            _saveData.Data[type] = 0;
        }
        
        _saveData.Data[type] += amount;
        _saveData.SetDirty();
        OnChange?.Invoke();
    }

    public bool TakeItems(ItemType type, int amount)
    {
        if (!_saveData.Data.TryGetValue(type, out var count) || count < amount)
        {
            return false;
        }

        if (count - amount > 0)
        {
            _saveData.Data[type] = count - amount;
        }
        else
        {
            _saveData.Data.Remove(type);
        }
        
        _saveData.SetDirty();
        OnChange?.Invoke();
        return true;
    }

    public int ItemsAmount(ItemType type)
    {
        _saveData.Data.TryGetValue(type, out var amount);
        return amount;
    }
}