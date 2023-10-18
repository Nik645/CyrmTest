using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingsController : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    [SerializeField] private BuildingView _buildingPrefab;
    [SerializeField] private Transform _buildingParrent;

    private ICommandDispatcher _commandDispatcher;
    private IBuildingsHolder _buildingsHolder;
    
    public void Init(ICommandDispatcher commandDispatcher, IBuildingsHolder buildingsHolder)
    {
        _commandDispatcher = commandDispatcher;
        _buildingsHolder = buildingsHolder;
    }

    private void Start()
    {
        CreateBuildings(_buildingsHolder.Buildings, BuildingType.Resource);
        CreateBuildings(_buildingsHolder.Buildings, BuildingType.Recycle);
        CreateBuildings(_buildingsHolder.Buildings, BuildingType.Market);
    }

    private void CreateBuildings(IEnumerable<IBuildingModel> buildings, BuildingType buildingType)
    {
        var uiData = _commandDispatcher.Execute(new GetBuildingUIQuery(buildingType));
        foreach (var model in buildings.Where(x => x.Type == buildingType))
        {
            var buildingView = Instantiate(_buildingPrefab, _buildingParrent, false);
            buildingView.SetData(uiData.Sprite, uiData.Name, model.BuildingAction);
            buildingView.transform.position = _grid.CellToWorld(model.Position);
        }
    }
}