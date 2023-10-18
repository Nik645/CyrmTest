using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingsSettings", menuName = "SO/BuildingsSettings")]
public class BuildingsSettings : ScriptableObject
{
    [SerializeField] private List<BuildingUISettings> _uiSettings;
    [SerializeField] private ResourceCraftSettings _resourceCraftSettings;
    [SerializeField] private RecycleCraftSettings _recycleCraftSettings;

    public List<BuildingUISettings> UISettings => _uiSettings;
    public ResourceCraftSettings ResourceCraftSettings => _resourceCraftSettings;
    public RecycleCraftSettings RecycleCraftSettings => _recycleCraftSettings;
}