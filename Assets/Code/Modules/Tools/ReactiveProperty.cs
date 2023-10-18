using System;

public interface IReadOnlyReactiveProperty<T> 
{
    T Value { get; }
    void Subscribe(Action<T> action);
    void Unsubscribe(Action<T> action);
}

public interface IReactiveProperty<T> : IReadOnlyReactiveProperty<T>, IDisposable
{
    T Value { get; set; }
}

public class ReactiveProperty<T> : IReactiveProperty<T>
{
    private Action<T> _onChange; 

    public T Value
    {
        get => _value;
        set
        {
            if (Equals(_value, value))
            {
                return;
            }

            _value = value;
            _onChange?.Invoke(_value);
        }
    }

    public void Subscribe(Action<T> action)
    {
        _onChange += action;
        action?.Invoke(Value);
    }

    public void Unsubscribe(Action<T> action)
    {
        _onChange -= action;
    }

    private T _value;

    public virtual void Dispose()
    {
        _onChange = null;
    }
}
