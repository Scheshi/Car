using System;
using System.Collections.Generic;
using Assets.Scripts.Configs;
using Assets.Scripts.Services;
using UnityEngine;


namespace Assets.Scripts.Features.Inventory
{
    public class InventoryView : MonoBehaviour, IInventoryView
    {
        private EventHandler<UsableItem> Selected;
        private EventHandler<UsableItem> Deselected;
        private UsableItem _currentChoosesSlot;
        [SerializeField] private Slot _cellPrefab;
        [SerializeField] private Transform _content;

        public void Init(Action<UsableItem> onSelected, Action<UsableItem> onDeselected)
        {
            Selected += delegate(object sender, UsableItem item)
            {
                onSelected.Invoke(item);
            };
            Deselected += delegate(object sender, UsableItem item)
            {
                onDeselected.Invoke(item);
            };
            Show();
        }

        public void Deinit()
        {
            Hide();
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
                    if (items[i1] == _currentChoosesSlot)
                    {
                        Deselected.Invoke(this, items[i1]);
                        _content.GetChild(i1).GetComponent<Slot>().Selected(false);
                        _currentChoosesSlot = null;
                    }
                    else
                    {
                        Selected.Invoke(this, items[i1]);
                        for (int y = 0; y < _content.childCount; y++)
                        {
                            if (y != i1)
                            {
                                _content.GetChild(y).GetComponent<Slot>().Selected(false);
                            }
                            else _content.GetChild(y).GetComponent<Slot>().Selected(true);
                        }
                        _currentChoosesSlot = items[i1];
                    }
                });
            }
        }

        private void OnDestroy()
        {
            Selected = null;
            Deselected = null;
        }

        public void Show()
        {
            _content.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _content.gameObject.SetActive(false);
        }
    }
}