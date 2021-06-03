using System;
using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;


namespace Assets.Scripts.Controllers
{
    public class CarController : BaseController
    {
        private const string ADDRESSABLES_LABEL = "Car";
        private Car _model;
        private CarView _view;
        public CarView CarObject => _view;

        public CarController(Car model)
        {
            _model = model;
        }

        public void Move(float value)
        {
            _view.ChangeSpeed(_model.Moving(value));
        }

        public void ChangeState(StateGame state)
        {
            switch (state)
            {
                case StateGame.Menu:
                    Dispose();
                    break;
                case StateGame.Game:
                    Debug.Log("Game");
                    Addressables.LoadAssetAsync<GameObject>(ADDRESSABLES_LABEL).Completed += OnCompleted;
                    break;
            }
        }

        private void OnCompleted(AsyncOperationHandle<GameObject> obj)
        {
            Debug.Log(nameof(OnCompleted));
            switch (obj.Status)
            {
                case AsyncOperationStatus.Succeeded:
                    _view = Object.Instantiate(obj.Result).GetComponent<CarView>();
                    AddGameObject(_view.gameObject);
                    break;
                case AsyncOperationStatus.None:
                    throw new ArgumentException("Нет законченных операций");
                    break;
                case AsyncOperationStatus.Failed:
                    throw new ArgumentException("Загрузка префаба не смогла выполниться");
            }

        }

        /// <summary>
        /// If you need moving car object
        /// </summary>
        /// <param name="speed">speed of moving object</param>
        public void ChangeSpeedCar(float speed)
        {
            _view.Rigidbody.AddForce(new Vector2(speed * _view.Rigidbody.mass * 10, 0), ForceMode2D.Impulse);
        }
    }
}