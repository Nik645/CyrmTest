public interface ISaveDataHolder
{
    bool Dirty { get; }
    void SetDirty();
    void Save();
}

public interface ISaveRemover
{
    void SaveRemove();
}