public class ShowDialogCommand : ICommand
{
    public IDialogModel Data { get; }

    public ShowDialogCommand(IDialogModel data)
    {
        Data = data;
    }
}

public class ShowDialogCommandHandler : CommandHandler<ShowDialogCommand>
{
    private readonly IDialogsHolder _dialogsHolder;

    public ShowDialogCommandHandler(IDialogsHolder dialogsHolder)
    {
        _dialogsHolder = dialogsHolder;
    }

    protected override void Execute(ShowDialogCommand command)
    {
        _dialogsHolder.Add(command.Data);
    }
}