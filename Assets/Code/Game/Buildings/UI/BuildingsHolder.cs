using System.Collections.Generic;

public class BuildingsHolder : IBuildingsHolder
{
    private readonly HashSet<IBuildingModel> _buildings = new HashSet<IBuildingModel>();

    public IEnumerable<IBuildingModel> Buildings => _buildings;
    
    public void Add(IBuildingModel buildingModel)
    {
        _buildings.Add(buildingModel);
    }
}