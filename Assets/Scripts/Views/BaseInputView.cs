using Assets.Scripts.Actions;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class BaseInputView : MonoBehaviour
    {
        private SubscriptionObserver<float> _rightMove;
        private SubscriptionObserver<float> _leftMove;
        protected float _speed;
        
        public virtual void Init(SubscriptionObserver<float> rightMove, SubscriptionObserver<float> leftMove, float speed)
        {
            _rightMove = rightMove;
            _leftMove = leftMove;
            _speed = speed;
        }

        protected virtual void OnLeftMove(float value)
        {
            _leftMove.Value = value;
        }

        protected virtual void OnRightMove(float value)
        {
            _rightMove.Value = value;
        }
    }
}