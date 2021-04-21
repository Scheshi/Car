using System;
using System.Collections.Generic;
using Assets.Scripts.Configs;
using Assets.Scripts.Services;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.Features.Inventory
{
    public class InventoryView : MonoBehaviour, IInventoryView
    {
        private EventHandler<UsableItem> Selected;
        private EventHandler<UsableItem> Deselected;
        [SerializeField] private Slot _cellPrefab;
        [SerializeField] private Button _button;
        [SerializeField] private Transform _content;
        private bool _isHide;

        public void Init(Action<UsableItem> onSelected, Action<UsableItem> onDeselected)
        {
            _button.onClick.AddListener(() =>
            {
                if(_isHide)
                    Show();
                else Hide(); 
            });
            Selected += delegate(object sender, UsableItem item)
            {
                onSelected.Invoke(item);
                Debug.Log("OnSelected" + item.Name);
            };
            Deselected += delegate(object sender, UsableItem item)
            {
                onDeselected.Invoke(item);
                Debug.Log("OnDeselected" + item.Name);
            };
        }

        public void Deinit()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void Build(List<UsableItem> items)
        {
            if (_content.childCount > items.Count)
            {
                for (int i = items.Count - 1; i < _content.childCount; i++)
                {
                    Destroy(_content.GetChild(i).gameObject);
                }
            }
            else if (_content.childCount < items.Count)
            {
                for (int i = _content.childCount; i < items.Count; i++)
                {
                    Instantiate(_cellPrefab, _content);
                }
            }
            for (int i = 0; i < items.Count; i++)
            {
                var i1 = i;
                _content.GetChild(i).GetComponent<Slot>().SetSlot(items[i].Sprite, items[i].Name, () =>
                {
                   Selected.Invoke(this, items[i1]);
                });
            }
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
            Selected = null;
            Deselected = null;
        }

        public void Show()
        {
            _content.gameObject.SetActive(true);
            _isHide = false;
        }

        public void Hide()
        {
            _isHide = true;
            _content.gameObject.SetActive(false);
        }
    }
}