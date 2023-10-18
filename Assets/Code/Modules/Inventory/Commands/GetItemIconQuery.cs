using System.Linq;
using UnityEngine;

public class GetItemIconQuery : ICommand<Sprite>
{
    public ItemType ItemType { get; }

    public GetItemIconQuery(ItemType itemType)
    {
        ItemType = itemType;
    }
}

public class GetItemIconQueryHandler : CommandHandler<GetItemIconQuery, Sprite>
{
    private readonly ItemsSettings _itemsUISettings;

    public GetItemIconQueryHandler(ItemsSettings itemsUISettings)
    {
        _itemsUISettings = itemsUISettings;
    }

    protected override Sprite Execute(GetItemIconQuery command)
    {
        return _itemsUISettings.UISettings.FirstOrDefault(x => x.Type == command.ItemType)?.Sprite;
    }
}