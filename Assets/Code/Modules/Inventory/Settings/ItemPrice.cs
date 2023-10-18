using System;
using UnityEngine;

[Serializable]
public class ItemPrice
{
    [SerializeField] private ItemType _type;
    [SerializeField] private CurrencyType _currency;
    [SerializeField] private int _price;

    public ItemType Type => _type;
    public CurrencyType Currency => _currency;
    public int Price => _price;
}