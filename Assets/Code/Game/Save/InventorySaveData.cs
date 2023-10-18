using System.Collections.Generic;

public class InventorySaveData : ISaveData<Dictionary<ItemType, int>>
{
    public Dictionary<ItemType, int> Data => _saveDataHolder.InventoryData;

    private readonly SaveDataHolder _saveDataHolder;

    public InventorySaveData(SaveDataHolder saveDataHolder)
    {
        _saveDataHolder = saveDataHolder;
    }

    public void SetDirty()
    {
        _saveDataHolder.SetDirty();
    }
}