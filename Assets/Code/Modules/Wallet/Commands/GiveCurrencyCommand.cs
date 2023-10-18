public class GiveCurrencyCommand : ICommand
{
    public CurrencyType CurrencyType { get; }
    public int Amount { get; }

    public GiveCurrencyCommand(CurrencyType currencyType, int amount)
    {
        CurrencyType = currencyType;
        Amount = amount;
    }
}

public class GiveCurrencyCommandHandler : CommandHandler<GiveCurrencyCommand>
{
    private readonly IWallet _wallet;

    public GiveCurrencyCommandHandler(IWallet wallet)
    {
        _wallet = wallet;
    }

    protected override void Execute(GiveCurrencyCommand command)
    {
        _wallet.GiveCurrency(command.CurrencyType, command.Amount);
    }
}