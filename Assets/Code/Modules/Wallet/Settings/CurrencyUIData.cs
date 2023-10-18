using System;
using UnityEngine;

[Serializable]
public class CurrencyUIData
{
    [SerializeField] private CurrencyType _type;
    [SerializeField] private Sprite _sprite;

    public CurrencyType Type => _type;
    public Sprite Sprite => _sprite;
}