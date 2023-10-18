using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsSettings", menuName = "SO/ItemsSettings")]
public class ItemsSettings : ScriptableObject
{
    [SerializeField] private List<ItemUIData> _uiSettings;
    [SerializeField] private List<ItemPrice> _itemPrices;
    
    public IEnumerable<ItemUIData> UISettings => _uiSettings;
    public IEnumerable<ItemPrice> ItemPrices => _itemPrices;
}