public interface IItemSwitchReactiveProperty : IReadOnlyReactiveProperty<ItemType>
{
    void SwitchItem();
    bool IsValid();
}