public class SaveImmediatelyCommand : ICommand
{
    
}

public class SaveImmediatelyCommandHandler : CommandHandler<SaveImmediatelyCommand>
{
    private readonly ISaveDataHolder _saveDataHolder;

    public SaveImmediatelyCommandHandler(ISaveDataHolder saveDataHolder)
    {
        _saveDataHolder = saveDataHolder;
    }

    protected override void Execute(SaveImmediatelyCommand command)
    {
        _saveDataHolder.Save();
    }
}