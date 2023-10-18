using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RecycleCraftSettings
{
    [SerializeField] private int _craftTime;
    [SerializeField] private int _resourcesCount;
    [SerializeField] private List<RecipeTool> _recipes;
    
    public int CraftTime => _craftTime;
    public int ResourcesCount => _resourcesCount;
    public IEnumerable<RecipeTool> Recipes => _recipes;
}

[Serializable]
public class RecipeTool
{
    [SerializeField] private List<ItemWithAmount> _resources;
    [SerializeField] private ItemWithAmount _tool;

    public IEnumerable<ItemWithAmount> Resources => _resources;
    public ItemWithAmount Tool => _tool;
}

[Serializable]
public struct ItemWithAmount
{
    public ItemType Item;
    public int Amount;
}
