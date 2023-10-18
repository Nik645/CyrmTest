using System;
using System.Collections.Generic;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly Dictionary<Type, object> _handlers = new Dictionary<Type, object>();
    
    public void AddCommandHandler<T,TResult>(ICommandHandler<T, TResult> handler) where T : ICommand<TResult>
    {
        _handlers[typeof(T)] = handler;
    }
    
    public void AddCommandHandler<T>(ICommandHandler<T> handler) where T : ICommand
    {
        _handlers[typeof(T)] = handler;
    }

    public void Execute<T>(T command) where T : ICommand
    {
        if (_handlers[typeof(T)] is ICommandHandler<T> handler)
        {
            handler.Execute(command);
        }
    }
    
    public TResult Execute<TResult>(ICommand<TResult> command)
    {
        if (_handlers[command.GetType()] is IExecutable<TResult> handler)
        {
            return handler.Execute(command);
        }
        
        return default;
    }
}