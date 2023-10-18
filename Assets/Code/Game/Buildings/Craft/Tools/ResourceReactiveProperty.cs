using System.Collections.Generic;
using System.Linq;

public class ResourceReactiveProperty : ReactiveProperty<ItemType>, IItemSwitchReactiveProperty
{
    private readonly List<ItemType> _resources = Constants.Resources.ToList();
    
    public void SwitchItem()
    {
        Value = _resources.GetNextItem(Value);
    }

    public bool IsValid()
    {
        return _resources.Contains(Value);
    }
}