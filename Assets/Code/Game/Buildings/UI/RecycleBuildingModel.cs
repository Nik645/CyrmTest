using UnityEngine;

public class RecycleBuildingModel : IBuildingModel
{
    public BuildingType Type => BuildingType.Recycle;
    public Vector3Int Position { get; }

    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IRecycleCrafter _recycleCrafter;

    public RecycleBuildingModel(Vector3Int position, ICommandDispatcher commandDispatcher, IRecycleCrafter recycleCrafter)
    {
        Position = position;
        _commandDispatcher = commandDispatcher;
        _recycleCrafter = recycleCrafter;
    }
    
    public void BuildingAction()
    {
        _commandDispatcher.Execute(new ShowDialogCommand(new RecycleDialogModel(_recycleCrafter, _commandDispatcher)));
    }
}