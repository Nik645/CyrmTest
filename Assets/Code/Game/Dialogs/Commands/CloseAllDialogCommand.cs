public class CloseAllDialogCommand : ICommand
{
}

public class CloseAllDialogCommandHandler : CommandHandler<CloseAllDialogCommand>
{
    private readonly IDialogsHolder _dialogsHolder;

    public CloseAllDialogCommandHandler(IDialogsHolder dialogsHolder)
    {
        _dialogsHolder = dialogsHolder;
    }
    
    protected override void Execute(CloseAllDialogCommand command)
    {
        while (_dialogsHolder.Current != null)
        {
            _dialogsHolder.Remove(_dialogsHolder.Current);
        }
    }
}