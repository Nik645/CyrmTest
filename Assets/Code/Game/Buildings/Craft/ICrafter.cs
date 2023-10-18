public interface ICrafter
{
    IReadOnlyReactiveProperty<bool> Active { get; }
    void Start();
    void Stop();
}