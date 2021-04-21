using System;
using System.Collections.Generic;
using Assets.Scripts.Configs;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.Features.Inventory
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private GameObject _cellPrefab;
        [SerializeField] private Button _button;
        [SerializeField] private Transform _content;
        private bool _isHide;
        public bool IsHide => _isHide;

        public void Init(Action action)
        {
            _button.onClick.AddListener(action.Invoke);
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
                _content.GetChild(i).GetComponent<Image>().sprite = items[i].Sprite;
            }
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