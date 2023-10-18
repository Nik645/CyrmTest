using UnityEngine;

public interface IItemSwitchViewModel
{
    IReadOnlyReactiveProperty<Sprite> Icon { get; }
    void SwitchItem();
}