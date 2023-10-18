using System.Collections.Generic;

public interface IBuildingsHolder
{
    IEnumerable<IBuildingModel> Buildings { get; }
    void Add(IBuildingModel buildingModel);
}