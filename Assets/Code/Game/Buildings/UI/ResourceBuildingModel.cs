using UnityEngine;

public class ResourceBuildingModel : IBuildingModel
{
    public BuildingType Type => BuildingType.Resource;
    public Vector3Int Position { get; }
    
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IResourceCrafter _resourceCrafter;

    public ResourceBuildingModel(Vector3Int position, ICommandDispatcher commandDispatcher, IResourceCrafter resourceCrafter)
    {
        Position = position;
        _commandDispatcher = commandDispatcher;
        _resourceCrafter = resourceCrafter;
    }
    
    public void BuildingAction()
    {
        _commandDispatcher.Execute(new ShowDialogCommand(new ResourceDialogModel(_resourceCrafter, _commandDispatcher)));
    }
}