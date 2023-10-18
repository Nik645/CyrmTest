public interface ICommandDispatcher
{
    void Execute<T>(T command) where T : ICommand;
    TResult Execute<TResult>(ICommand<TResult> command);
}