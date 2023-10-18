public class AutoSaveService : ITick
{
    private readonly ISaveDataHolder _saveDataHolder;

    public AutoSaveService(ISaveDataHolder saveDataHolder)
    {
        _saveDataHolder = saveDataHolder;
    }

    public void Tick()
    {
        if (_saveDataHolder.Dirty)
        {
            _saveDataHolder.Save();
        }
    }
}