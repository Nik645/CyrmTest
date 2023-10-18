public class GetItemAmountQuery : ICommand<int>
{
    public ItemType ItemType { get; }

    public GetItemAmountQuery(ItemType itemType)
    {
        ItemType = itemType;
    }
}

public class GetItemAmountQueryHandler : CommandHandler<GetItemAmountQuery, int>
{
    private readonly IInventory _inventory;

    public GetItemAmountQueryHandler(IInventory inventory)
    {
        _inventory = inventory;
    }
    
    protected override int Execute(GetItemAmountQuery command)
    {
        return _inventory.ItemsAmount(command.ItemType);
    }
}