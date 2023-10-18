using UnityEngine;

[CreateAssetMenu(fileName = "WinGameSettings", menuName = "SO/WinGameSettings")]
public class WinGameSettings : ScriptableObject
{
    [SerializeField] private CurrencyType _currencyType;
    [SerializeField] private int _amount;

    public CurrencyType CurrencyType => _currencyType;
    public int Amount => _amount;
}
