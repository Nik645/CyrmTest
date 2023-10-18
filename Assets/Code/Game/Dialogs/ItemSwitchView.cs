using UnityEngine;
using UnityEngine.UI;

namespace Code.Dialogs
{
    public class ItemSwitchView : MonoBehaviour
    {
        [SerializeField] private Button _switchItemButton;
        [SerializeField] private Image _itemIcon;

        private IItemSwitchViewModel _model;
    
        public void SetData(IItemSwitchViewModel model)
        {
            _model = model;
        }
    
        private void Start()
        {
            _model.Icon.Subscribe(SetResourceIcon);
            _switchItemButton.onClick.AddListener(HandleSwitchItemOnClick);
        }

        private void OnDestroy()
        {
            _model.Icon.Unsubscribe(SetResourceIcon);
            _switchItemButton.onClick.RemoveListener(HandleSwitchItemOnClick);
        }

        private void HandleSwitchItemOnClick()
        {
            _model.SwitchItem();
        }
    
        private void SetResourceIcon(Sprite sprite)
        {
            _itemIcon.sprite = sprite;
        }
    }
}