public abstract class CommandHandler<T, TResult> : ICommandHandler<T, TResult> where T : ICommand<TResult>
{
    public TResult Execute(ICommand<TResult> command)
    {
        return Execute((T) command);
    }

    protected abstract TResult Execute(T command);
}

public abstract class CommandHandler<T> : ICommandHandler<T> where T : ICommand
{
    public void Execute(ICommand command)
    {
        Execute((T) command);
    }

    protected abstract void Execute(T command);
}