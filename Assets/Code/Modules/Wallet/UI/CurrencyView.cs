using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _amount;

    public void SetIcon(Sprite sprite)
    {
        _icon.gameObject.SetActive(sprite != null);
        _icon.sprite = sprite;
    }

    public void SetAmount(string amount)
    {
        _amount.text = amount;
    }
}