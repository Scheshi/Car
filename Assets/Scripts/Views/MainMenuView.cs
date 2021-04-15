using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Dropdown _dropdownChooseInput;
        [SerializeField] private TrailRenderer _trailRenderer;
        private Camera _camera;

        public void Init(Action actionStartGame, Action<int> chooseInput)
        {
            _camera = Camera.main;
            _buttonStart.onClick.AddListener(() =>
            {
                actionStartGame.Invoke();
                gameObject.SetActive(false);
            });
            _dropdownChooseInput.onValueChanged.AddListener(chooseInput.Invoke);

            GameUpdater.Instance.Add(TrailTouch);
        }
        protected void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
#if UNITY_ANDROID || UNITY_IOS
            GameUpdater.Instance.Remove(TrailTouch);
#endif
        }

        protected void TrailTouch()
        {
#if UNITY_EDITOR
            var position = _camera.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0.0f;
            _trailRenderer.transform.position = position;
#elif UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount > 0)
            {
                var touch = Input.touches[0];
                var touchPosition = _camera.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0.0f;
                _trailRenderer.transform.position = touchPosition;
            }
#endif
        }
    }
}