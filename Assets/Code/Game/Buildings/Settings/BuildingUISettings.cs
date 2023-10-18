using System;
using UnityEngine;

[Serializable]
public class BuildingUISettings
{
    [SerializeField] private BuildingType _type;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _name;

    public BuildingType Type => _type;
    public Sprite Sprite => _sprite;
    public string Name => _name;
}