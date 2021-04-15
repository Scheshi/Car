using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Dropdown _dropdownChooseInput;

        public void Init(Action actionStartGame, Action<int> chooseInput)
        {
            _buttonStart.onClick.AddListener(() =>
            {
                actionStartGame.Invoke();
                gameObject.SetActive(false);
            });
            _dropdownChooseInput.onValueChanged.AddListener(chooseInput.Invoke);
        }

        protected void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
        }
    }
}