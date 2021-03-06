using System;
using Assets.Scripts.Configs;
using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Features.Garage
{
    public class GarageView : MonoBehaviour
    {
        [SerializeField] private Image _speedItem;
        [SerializeField] private Text _speedItemName;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _backToMenuButton;

        public void Init(Action upgradeAction, Action backToMenu)
        {
            _upgradeButton.onClick.AddListener(upgradeAction.Invoke);
            _backToMenuButton.onClick.AddListener(backToMenu.Invoke);
        }

        public void SetSlot(UsableItem item)
        {
            switch (item.UpgradeType)
            {
                case UpgradableType.Speed:
                    _speedItem.sprite = item.Sprite;
                    _speedItemName.text = item.Name;
                    break;
            }
        }

        private void OnDestroy()
        {
            _upgradeButton?.onClick.RemoveAllListeners();
        }
    }
}