using UnityEngine;

public class MarketBuildingModel : IBuildingModel
{
    public BuildingType Type => BuildingType.Market;
    public Vector3Int Position { get; }

    private ICommandDispatcher _commandDispatcher;
    private IInventory _inventory;

    public MarketBuildingModel(Vector3Int position, ICommandDispatcher commandDispatcher, IInventory inventory)
    {
        Position = position;
        _commandDispatcher = commandDispatcher;
        _inventory = inventory;
    }

    public void BuildingAction()
    {
        _commandDispatcher.Execute(new ShowDialogCommand(new MarketDialogModel(_commandDispatcher, _inventory)));
    }
}