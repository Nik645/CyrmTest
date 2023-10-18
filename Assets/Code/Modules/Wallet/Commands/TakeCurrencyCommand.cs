public class TakeCurrencyCommand : ICommand<bool>
{
    public CurrencyType CurrencyType { get; }
    public int Amount { get; }

    public TakeCurrencyCommand(CurrencyType currencyType, int amount)
    {
        CurrencyType = currencyType;
        Amount = amount;
    }
}

public class TakeCurrencyCommandHandler : CommandHandler<TakeCurrencyCommand, bool>
{
    private readonly IWallet _wallet;

    public TakeCurrencyCommandHandler(IWallet wallet)
    {
        _wallet = wallet;
    }

    protected override bool Execute(TakeCurrencyCommand command)
    {
        if (_wallet.CurrencyAmount(command.CurrencyType) < command.Amount)
        {
            return false;
        }

        _wallet.TakeCurrency(command.CurrencyType, command.Amount);
        return true;
    }
}