public class TakeItemCommand : ICommand<bool>
{
    public ItemType ItemType { get; }
    public int Amount { get; }

    public TakeItemCommand(ItemType itemType, int amount)
    {
        ItemType = itemType;
        Amount = amount;
    }
}

public class TakeItemCommandHandler : CommandHandler<TakeItemCommand, bool>
{
    private readonly IInventory _inventory;

    public TakeItemCommandHandler(IInventory inventory)
    {
        _inventory = inventory;
    }

    protected override bool Execute(TakeItemCommand command)
    {
        if (_inventory.ItemsAmount(command.ItemType) < command.Amount)
        {
            return false;
        }

        _inventory.TakeItems(command.ItemType, command.Amount);
        return true;
    }
}