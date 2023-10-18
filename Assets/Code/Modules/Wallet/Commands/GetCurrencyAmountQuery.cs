public class GetCurrencyAmountQuery : ICommand<int>
{
    public CurrencyType CurrencyType { get; }

    public GetCurrencyAmountQuery(CurrencyType currencyType)
    {
        CurrencyType = currencyType;
    }
}

public class GetCurrencyAmountQueryHandler : CommandHandler<GetCurrencyAmountQuery, int>
{
    private readonly IWallet _wallet;

    public GetCurrencyAmountQueryHandler(IWallet wallet)
    {
        _wallet = wallet;
    }

    protected override int Execute(GetCurrencyAmountQuery command)
    {
        return _wallet.CurrencyAmount(command.CurrencyType);
    }
}