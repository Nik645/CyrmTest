using System.Linq;
using UnityEngine;

public class GetCurrencyIconQuery : ICommand<Sprite>
{
    public CurrencyType CurrencyType { get; }

    public GetCurrencyIconQuery(CurrencyType currencyType)
    {
        CurrencyType = currencyType;
    }
}

public class GetCurrencyIconQueryHandler : CommandHandler<GetCurrencyIconQuery, Sprite>
{
    private readonly CurrencySettings _currencySettings;

    public GetCurrencyIconQueryHandler(CurrencySettings currencySettings)
    {
        _currencySettings = currencySettings;
    }

    protected override Sprite Execute(GetCurrencyIconQuery command)
    {
        return _currencySettings.UISettings.FirstOrDefault(x => x.Type == command.CurrencyType)?.Sprite;
    }
}