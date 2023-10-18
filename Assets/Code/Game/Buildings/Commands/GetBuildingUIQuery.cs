using System.Linq;

public class GetBuildingUIQuery : ICommand<BuildingUISettings>
{
    public BuildingType BuildingType { get; }

    public GetBuildingUIQuery(BuildingType buildingType)
    {
        BuildingType = buildingType;
    }
}

public class GetBuildingUIQueryHandler : CommandHandler<GetBuildingUIQuery, BuildingUISettings>
{
    private readonly BuildingsSettings _buildingsSettings;

    public GetBuildingUIQueryHandler(BuildingsSettings buildingsSettings)
    {
        _buildingsSettings = buildingsSettings;
    }

    protected override BuildingUISettings Execute(GetBuildingUIQuery command)
    {
        return _buildingsSettings.UISettings.FirstOrDefault(x => x.Type == command.BuildingType);
    }
}