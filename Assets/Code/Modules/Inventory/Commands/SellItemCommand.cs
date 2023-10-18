public class SellItemCommand : ICommand<bool>
{
    public ItemType ItemType { get; }
    public int Amount { get; }

    public SellItemCommand(ItemType itemType, int amount)
    {
        ItemType = itemType;
        Amount = amount;
    }
}

public class SellItemCommandHandler : CommandHandler<SellItemCommand, bool>
{
    private readonly ICommandDispatcher _commandDispatcher;

    public SellItemCommandHandler(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    protected override bool Execute(SellItemCommand command)
    {
        var itemPrice = _commandDispatcher.Execute(new GetItemPriceQuery(command.ItemType));
        if (itemPrice == null)
        {
            return false;
        }
        
        var taked = _commandDispatcher.Execute(new TakeItemCommand(command.ItemType, command.Amount));
        if (!taked)
        {
            return false;
        }

        _commandDispatcher.Execute(new GiveCurrencyCommand(itemPrice.Currency, itemPrice.Price));
        return true;
    }
}