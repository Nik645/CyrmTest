public interface ISaveData<T>
{
    T Data { get; }
    void SetDirty();
}