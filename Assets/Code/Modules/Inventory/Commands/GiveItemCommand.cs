public class GiveItemCommand : ICommand
{
    public ItemType ItemType { get; }
    public int Amount { get; }

    public GiveItemCommand(ItemType itemType, int amount)
    {
        ItemType = itemType;
        Amount = amount;
    }
}

public class GiveItemCommandHandler : CommandHandler<GiveItemCommand>
{
    private readonly IInventory _inventory;

    public GiveItemCommandHandler(IInventory inventory)
    {
        _inventory = inventory;
    }

    protected override void Execute(GiveItemCommand command)
    {
        _inventory.GiveItems(command.ItemType, command.Amount);
    }
}