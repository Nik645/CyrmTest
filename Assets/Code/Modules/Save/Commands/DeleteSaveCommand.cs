public class DeleteSaveCommand : ICommand
{
}

public class DeleteSaveCommandHandler : CommandHandler<DeleteSaveCommand>
{
    private readonly ISaveRemover _saveRemover;

    public DeleteSaveCommandHandler(ISaveRemover saveRemover)
    {
        _saveRemover = saveRemover;
    }

    protected override void Execute(DeleteSaveCommand command)
    {
        _saveRemover.SaveRemove();
    }
}