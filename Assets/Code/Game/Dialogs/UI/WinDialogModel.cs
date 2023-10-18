using System;

public interface IWinDialogModel : IDialogModel
{
    Action EndGame { get; }
}

public class WinDialogModel : IWinDialogModel
{
    public Action EndGame { get; }
    public DialogsType Type => DialogsType.WinDialog;
    public WinDialogModel(Action endGame)
    {
        EndGame = endGame;
    }
}