using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;

        public void Init(Action action)
        {
            _buttonStart.onClick.AddListener(() =>
            {
                action.Invoke();
                gameObject.SetActive(false);
            });
        }

        protected void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
        }
    }
}