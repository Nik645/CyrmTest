using System.Linq;

public class GetItemPriceQuery : ICommand<ItemPrice>
{
    public ItemType ItemType { get; }

    public GetItemPriceQuery(ItemType itemType)
    {
        ItemType = itemType;
    }
}

public class GetItemPriceQueryHandler : CommandHandler<GetItemPriceQuery, ItemPrice>
{
    private readonly ItemsSettings _itemsSettings;

    public GetItemPriceQueryHandler(ItemsSettings itemsSettings)
    {
        _itemsSettings = itemsSettings;
    }

    protected override ItemPrice Execute(GetItemPriceQuery command)
    {
        return _itemsSettings.ItemPrices.FirstOrDefault(x => x.Type == command.ItemType);
    }
}