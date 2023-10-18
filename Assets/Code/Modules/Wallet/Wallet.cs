using System;
using System.Collections.Generic;

public interface IWallet
{
    event Action OnChange;
    public IReadOnlyDictionary<CurrencyType, int> Currencies { get; }
    void GiveCurrency(CurrencyType type, int amount);
    bool TakeCurrency(CurrencyType type, int amount);
    int CurrencyAmount(CurrencyType type);
}

public class Wallet : IWallet
{
    public event Action OnChange;

    public IReadOnlyDictionary<CurrencyType, int> Currencies => _saveData.Data;
    private readonly ISaveData<Dictionary<CurrencyType, int>> _saveData;

    public Wallet(ISaveData<Dictionary<CurrencyType, int>> saveData)
    {
        _saveData = saveData;
    }

    public void GiveCurrency(CurrencyType type, int amount)
    {
        if (!_saveData.Data.ContainsKey(type))
        {
            _saveData.Data[type] = 0;
        }
        
        _saveData.Data[type] += amount;
        OnChange?.Invoke();
    }

    public bool TakeCurrency(CurrencyType type, int amount)
    {
        if (!_saveData.Data.TryGetValue(type, out var count) || count < amount)
        {
            return false;
        }

        _saveData.Data[type] = count - amount;
        OnChange?.Invoke();
        return true;
    }

    public int CurrencyAmount(CurrencyType type)
    {
        _saveData.Data.TryGetValue(type, out var amount);
        return amount;
    }
}