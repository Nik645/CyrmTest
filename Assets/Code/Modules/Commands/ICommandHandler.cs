public interface ICommandHandler<T>
{
    void Execute(ICommand command);
}

public interface IExecutable<TResult>
{
    TResult Execute(ICommand<TResult> command);
}

public interface ICommandHandler<T, TResult> : IExecutable<TResult>
{
}