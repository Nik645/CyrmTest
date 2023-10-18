using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _amount;

    public void SetIcon(Sprite sprite)
    {
        _icon.sprite = sprite;
    }

    public void SetAmount(string amount)
    {
        _amount.text = amount;
    }
}
