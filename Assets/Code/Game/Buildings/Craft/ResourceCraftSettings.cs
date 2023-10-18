using System;
using UnityEngine;

[Serializable]
public class ResourceCraftSettings
{
    [SerializeField] private int _craftTime;
    [SerializeField] private int _craftAmount;

    public int CraftTime => _craftTime;
    public int CraftAmount => _craftAmount;
}