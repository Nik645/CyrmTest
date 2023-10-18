using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrencySettings", menuName = "SO/CurrencySettings")]
public class CurrencySettings : ScriptableObject
{
    [SerializeField] private List<CurrencyUIData> _uiSettings;

    public List<CurrencyUIData> UISettings => _uiSettings;
}