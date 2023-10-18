using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameSceneInstaller : MonoBehaviour
{
    [SerializeField] private BuildingsSettings _buildingSettings;
    [SerializeField] private BuildingsController _buildingsController;
    [SerializeField] private DialogsSettings _dialogsSettings;
    [SerializeField] private DialogsController _dialogsController;
    [SerializeField] private InventoryController _inventoryController;
    [SerializeField] private WalletController _walletController;
    [SerializeField] private ItemsSettings _itemsSettings;
    [SerializeField] private CurrencySettings _currencySettings;
    [SerializeField] private WinGameSettings _winGameSettings;
    
    private readonly DialogsHolder _dialogsHolder = new();
    private readonly CommandDispatcher _commandDispatcher = new();
    private readonly HashSet<IDisposable> _disposables = new();
    private SaveDataHolder _saveDataHolder;
    private Inventory _inventory;
    private Wallet _wallet;
    private InventorySaveData _inventorySaveData;
    private WalletSaveData _walletSaveData;
    private TickService _tickService;
    private BuildingsHolder _buildingsHolder;
    private WinGameService _winService;

    private void Awake()
    {
        _tickService = new GameObject().AddComponent<TickService>();
        _saveDataHolder = new SaveDataHolder();
        _inventorySaveData = new InventorySaveData(_saveDataHolder);
        _walletSaveData = new WalletSaveData(_saveDataHolder);
        _tickService.Add(new AutoSaveService(_saveDataHolder));

        _inventory = new Inventory(_inventorySaveData);
        _wallet = new Wallet(_walletSaveData);

        _buildingsHolder = new BuildingsHolder();

        var resourceBuildingsPosition = new List<Vector3Int>()
        {
            new(4, 8, 0),
            new(6, 6, 0),
            new(8, 4, 0)
        };

        foreach (var pos in resourceBuildingsPosition.Take(BuildingsInitData.ResourceBuildingsNumber))
        {
            AddResourceBuilding(pos);
        }

        AddRecycleBuilding(new Vector3Int(-2, 2, 0));
        AddMarketBuilding(new Vector3Int(2, -2, 0));

        _buildingsController.Init(_commandDispatcher, _buildingsHolder);
        _dialogsController.Init(_dialogsHolder, new DialogsFactory(_dialogsSettings));
        _inventoryController.Init(_inventory, _commandDispatcher);
        _walletController.Init(_wallet, _commandDispatcher);
        _commandDispatcher.AddCommandHandler(new SaveImmediatelyCommandHandler(_saveDataHolder));
        _commandDispatcher.AddCommandHandler(new DeleteSaveCommandHandler(_saveDataHolder));
        _commandDispatcher.AddCommandHandler(new ShowDialogCommandHandler(_dialogsHolder));
        _commandDispatcher.AddCommandHandler(new CloseAllDialogCommandHandler(_dialogsHolder));
        _commandDispatcher.AddCommandHandler(new GiveItemCommandHandler(_inventory));
        _commandDispatcher.AddCommandHandler(new TakeItemCommandHandler(_inventory));
        _commandDispatcher.AddCommandHandler(new GetItemAmountQueryHandler(_inventory));
        _commandDispatcher.AddCommandHandler(new GiveCurrencyCommandHandler(_wallet));
        _commandDispatcher.AddCommandHandler(new TakeCurrencyCommandHandler(_wallet));
        _commandDispatcher.AddCommandHandler(new GetCurrencyIconQueryHandler(_currencySettings));
        _commandDispatcher.AddCommandHandler(new GetCurrencyAmountQueryHandler(_wallet));
        _commandDispatcher.AddCommandHandler(new GetItemPriceQueryHandler(_itemsSettings));
        _commandDispatcher.AddCommandHandler(new SellItemCommandHandler(_commandDispatcher));
        _commandDispatcher.AddCommandHandler(new GetItemIconQueryHandler(_itemsSettings));
        _commandDispatcher.AddCommandHandler(new GetBuildingUIQueryHandler(_buildingSettings));

        _winService = new WinGameService(_wallet, _commandDispatcher, _winGameSettings);
        _disposables.Add(_winService);
    }

    private void OnDestroy()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
        _disposables.Clear();
        Destroy(_tickService);
    }
    
    private void AddResourceBuilding(Vector3Int pos)
    {
        var crafter = new ResourceCrafter(_buildingSettings.ResourceCraftSettings, _commandDispatcher, _tickService);
        _disposables.Add(crafter);
        _tickService.Add(crafter);
        _buildingsHolder.Add(new ResourceBuildingModel(pos, _commandDispatcher, crafter));
    }

    private void AddRecycleBuilding(Vector3Int pos)
    {
        var crafter = new RecycleCrafter(_buildingSettings.RecycleCraftSettings, _commandDispatcher, _tickService);
        _disposables.Add(crafter);
        _tickService.Add(crafter);
        _buildingsHolder.Add(new RecycleBuildingModel(pos, _commandDispatcher, crafter));
    }

    private void AddMarketBuilding(Vector3Int pos)
    {
        var market = new MarketBuildingModel(pos, _commandDispatcher, _inventory);
        _buildingsHolder.Add(market);
    }
}