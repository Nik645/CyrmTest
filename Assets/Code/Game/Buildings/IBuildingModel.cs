using UnityEngine;

public interface IBuildingModel
{ 
    BuildingType Type { get; }
    Vector3Int Position { get; }
    void BuildingAction();
}