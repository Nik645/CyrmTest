using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryItemView _prefab;
    [SerializeField] private Transform _itemHolder;

    private IInventory _inventory;
    private ICommandDispatcher _commandDispatcher;

    private Dictionary<ItemType, InventoryItemView> _items = new Dictionary<ItemType, InventoryItemView>();

    public void Init(IInventory inventory, ICommandDispatcher commandDispatcher)
    {
        _inventory = inventory;
        _commandDispatcher = commandDispatcher;
    }

    private void Start()
    {
        _inventory.OnChange += HandleInventoryOnChange;
        UpdateItems();
    }

    private void OnDestroy()
    {
        _inventory.OnChange -= HandleInventoryOnChange;
    }

    private void HandleInventoryOnChange()
    {
        UpdateItems();
    }

    public void UpdateItems()
    {
        var inventoryItems = _inventory.Items.OrderBy(x=>x.Key);
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
                view.SetIcon(_commandDispatcher.Execute(new GetItemIconQuery(itemType)));
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
