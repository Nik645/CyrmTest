using System;
using UnityEngine.SceneManagement;

public class WinGameService : IDisposable
{
    private readonly IWallet _wallet;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly WinGameSettings _winGameSettings;
    
    public WinGameService(IWallet wallet, ICommandDispatcher commandDispatcher, WinGameSettings winGameSettings)
    {
        _wallet = wallet;
        _commandDispatcher = commandDispatcher;
        _wallet.OnChange += HandleWalletOnChange;
        _winGameSettings = winGameSettings;
        CheckWinConditions();
    }

    private void HandleWalletOnChange()
    {
        CheckWinConditions();
    }

    public void Dispose()
    {
        _wallet.OnChange -= HandleWalletOnChange;
    }

    private void CheckWinConditions()
    {
        if (_wallet.CurrencyAmount(_winGameSettings.CurrencyType) >= _winGameSettings.Amount)
        {
            _commandDispatcher.Execute(new CloseAllDialogCommand());
            _commandDispatcher.Execute(new ShowDialogCommand(new WinDialogModel(EndGame)));
        }
    }

    private void EndGame()
    {
        _commandDispatcher.Execute(new DeleteSaveCommand());
        SceneManager.LoadScene(Scene.StartScene.ToString(), LoadSceneMode.Single);
    }
}