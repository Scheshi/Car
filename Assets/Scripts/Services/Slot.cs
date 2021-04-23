using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Services
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private Text _text;

        public void SetSlot(Sprite sprite, string text, Action action)
        {
            _image.sprite = sprite;
            _text.text = text;
            _button.onClick.AddListener(action.Invoke);
        }

        public void SetInteractiveButton(bool isInteractive)
        {
            _button.interactable = isInteractive;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}