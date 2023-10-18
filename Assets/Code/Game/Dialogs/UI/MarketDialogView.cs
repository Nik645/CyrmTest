using Code.Dialogs;
using UnityEngine;
using UnityEngine.UI;

public class MarketDialogView : DialogView<IMarketDialogModel>
{
    [SerializeField] private ItemSwitchView _itemSwitchView;
    [SerializeField] private CurrencyView _price;
    [SerializeField] private Button _sellButton;

    private IMarketDialogModel _data;
    
    protected override void SetData(IMarketDialogModel data)
    {
        _data = data;
        _itemSwitchView.SetData(data.ItemModel);
    }

    protected override void Start()
    {
        base.Start();
        _data.Price.Subscribe(HandlePriceChange);
        _data.CurrencyIcon.Subscribe(HandleCurrencyIcon);
        _sellButton.onClick.AddListener(HandleSellOnClick);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _data.Price.Unsubscribe(HandlePriceChange);
        _data.CurrencyIcon.Unsubscribe(HandleCurrencyIcon);
        _sellButton.onClick.RemoveListener(HandleSellOnClick);
    }

    private void HandlePriceChange(string price)
    {
        _price.SetAmount(price);
    }

    private void HandleCurrencyIcon(Sprite sprite)
    {
        _price.SetIcon(sprite);
    }

    private void HandleSellOnClick()
    {
        _data.SellItem();
    }
}