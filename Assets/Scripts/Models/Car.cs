using System;
using Assets.Scripts.Actions;

namespace Assets.Scripts
{
    public class Car
    {
        public float Speed => _currentSpeed;
        private float _x;
        private float _currentSpeed;
        private float _maxSpeed;

        public Car(float maxSpeed, float x)
        {
            _maxSpeed = maxSpeed;
            _x = x;
            _currentSpeed = 0;
        }

        public float Moving(float rightMove)
        {
            if (_currentSpeed <= _maxSpeed)
            {
                if (rightMove > 0)
                {
                    _currentSpeed += _x;
                }
                else if(_currentSpeed > 0)
                {
                    _currentSpeed -= _x;
                }
            }
            else _currentSpeed = _maxSpeed;

            return _currentSpeed;
        }
    }
}