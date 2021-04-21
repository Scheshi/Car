using Assets.Scripts.Actions;
using UnityEngine;


namespace Assets.Scripts.Inputer.Views
{
    public class AccelerationInputView : BaseInputView
    {
        public override void Init(SubscriptionObserver<float> rightMove, SubscriptionObserver<float> leftMove, float speed)
        {
            base.Init(rightMove, leftMove, speed);
            GameUpdater.Instance.Add(Move);
        }

        private void Move()
        {
            var direction = Vector3.zero;
            direction.x = -Input.acceleration.y;
            direction.z = Input.acceleration.x;

            if (direction.sqrMagnitude > 1) direction.Normalize();
            OnRightMove(direction.sqrMagnitude * _speed * Time.deltaTime);
        }

        private void OnDestroy()
        {
            GameUpdater.Instance.Remove(Move);
        }
    }
}