using Assets.Scripts.Actions;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Assets.Scripts.Inputer.Views
{
    public class GyroscopeInputView : BaseInputView
    {
        public override void Init(SubscriptionObserver<float> rightMove, SubscriptionObserver<float> leftMove, float speed)
        {
            base.Init(rightMove, leftMove, speed);
            Input.gyro.enabled = true;
            GameUpdater.Instance.Add(Move);
        }

        private void OnDestroy()
        {
            GameUpdater.Instance.Remove(Move);
        }

        private void Move()
        {
            if (!SystemInfo.supportsGyroscope) return;
            Quaternion quaternion = Input.gyro.attitude;
            quaternion.Normalize();
            OnRightMove((quaternion.x + quaternion.y) * _speed * Time.deltaTime);
        }
    }
}