using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class SaveDataHolder : ISaveDataHolder, ISaveRemover
{
    public Dictionary<ItemType, int> InventoryData => _saveData.InventoryData;
    public Dictionary<CurrencyType, int> WalletData => _saveData.WalletData;

    public bool Dirty { get; private set; }

    private static readonly string SAVE_DIRECTORY = "save";
    private static readonly string SAVE_PATH = Path.Combine(SAVE_DIRECTORY, "save.json");

    private SaveData _saveData = new SaveData();
    
    public SaveDataHolder()
    {
        Load();
    }
    
    public void SetDirty()
    {
        Dirty = true;
    }

    public void Save()
    {
        var directoryPath = Path.Combine(Application.persistentDataPath, SAVE_DIRECTORY);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        var saveFilePath = Path.Combine(Application.persistentDataPath, SAVE_PATH);
        File.WriteAllText(saveFilePath, JsonConvert.SerializeObject(_saveData));
        Dirty = false;
    }

    private void Load()
    {
        var saveFilePath = Path.Combine(Application.persistentDataPath, SAVE_PATH);
        if (File.Exists(saveFilePath))
        {
            _saveData = JsonConvert.DeserializeObject<SaveData>(File.ReadAllText(saveFilePath));
        }
        else
        {
            _saveData = new SaveData();
        }
    }

    public void SaveRemove()
    {
        _saveData = new SaveData();
        var saveFilePath = Path.Combine(Application.persistentDataPath, SAVE_PATH);
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }
    }
}

[Serializable]
public class SaveData
{
    public Dictionary<ItemType, int> InventoryData = new();
    public Dictionary<CurrencyType, int> WalletData = new() {{CurrencyType.Coin, 0}};
}