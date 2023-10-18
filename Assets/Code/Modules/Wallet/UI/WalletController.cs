using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WalletController : MonoBehaviour
{
    [SerializeField] private CurrencyView _prefab;
    [SerializeField] private Transform _itemHolder;

    private IWallet _wallet;
    private ICommandDispatcher _commandDispatcher;

    private Dictionary<CurrencyType, CurrencyView> _items = new Dictionary<CurrencyType, CurrencyView>();

    public void Init(IWallet wallet, ICommandDispatcher commandDispatcher)
    {
        _wallet = wallet;
        _commandDispatcher = commandDispatcher;
    }

    private void Start()
    {
        _wallet.OnChange += HandleInventoryOnChange;
        UpdateItems();
    }

    private void OnDestroy()
    {
        _wallet.OnChange -= HandleInventoryOnChange;
    }

    private void HandleInventoryOnChange()
    {
        UpdateItems();
    }

    public void UpdateItems()
    {
        var inventoryItems = _wallet.Currencies.OrderBy(x=>x.Key);
        var toDelete = _items.Keys.ToList();

        foreach (var inventoryItem in inventoryItems)
        {
            var itemType = inventoryItem.Key;
            if (_items.TryGetValue(itemType, out var view))
            {
                toDelete.Remove(itemType);
            }
            else
            {
                view = Instantiate(_prefab, _itemHolder, false);
                view.SetIcon(_commandDispatcher.Execute(new GetCurrencyIconQuery(itemType)));
                _items[itemType] = view;
            }

            view.SetAmount(inventoryItem.Value.ToString());
            view.transform.SetAsLastSibling();
        }
        
        foreach (var itemType in toDelete)
        {
            var view = _items[itemType];
            _items.Remove(itemType);
            Destroy(view.gameObject);
        }
    }
}