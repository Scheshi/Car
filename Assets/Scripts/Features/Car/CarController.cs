using Assets.Scripts.Enums;
using UnityEngine;


namespace Assets.Scripts.Controllers
{
    public class CarController : BaseController
    {
        private string _pathToPrefab = "Prefabs/car";
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
                    _view = LoadView<CarView>(_pathToPrefab);
                    AddGameObject(_view.gameObject);
                    break;
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