using System.Collections.Generic;

public class WalletSaveData : ISaveData<Dictionary<CurrencyType, int>>
{
    public Dictionary<CurrencyType, int> Data => _saveDataHolder.WalletData;

    private readonly SaveDataHolder _saveDataHolder;

    public WalletSaveData(SaveDataHolder saveDataHolder)
    {
        _saveDataHolder = saveDataHolder;
    }

    public void SetDirty()
    {
        _saveDataHolder.SetDirty();
    }
}