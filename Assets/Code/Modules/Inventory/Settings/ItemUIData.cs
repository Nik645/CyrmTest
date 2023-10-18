using System;
using UnityEngine;

[Serializable]
public class ItemUIData
{
    [SerializeField] private ItemType _type;
    [SerializeField] private Sprite _sprite;

    public ItemType Type => _type;
    public Sprite Sprite => _sprite;
}