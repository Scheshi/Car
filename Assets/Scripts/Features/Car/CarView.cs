using Assets.Scripts.Actions;
using UnityEngine;

namespace Assets.Scripts
{
    public class CarView : MonoBehaviour
    {
        [SerializeField] private HingeJoint2D[] _wheels;
        private SubscriptionObserver<float> _speedObserver;
        [SerializeField]private Rigidbody2D _rigidbody2D;
        public Rigidbody2D Rigidbody => _rigidbody2D;

        public void Init(SubscriptionObserver<float> speedObserver)
        {
            _speedObserver = speedObserver;
            _speedObserver.SubscribeObserver(ChangeSpeed);
        }

        private void OnDestroy()
        {
            if (_speedObserver != null)
            {
                _speedObserver.UnSubscribeObserver(ChangeSpeed);
                _speedObserver = null;
            }
        }

        public void ChangeSpeed(float speed)
        {
            foreach (var wheel in _wheels)
            {
                var motor = wheel.motor;
                motor.motorSpeed = speed;
                wheel.motor = motor;
            }
        }
    }
}