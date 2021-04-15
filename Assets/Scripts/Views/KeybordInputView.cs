using Assets.Scripts.Actions;
using UnityEngine;

namespace Assets.Scripts
{
    public class KeybordInputView : BaseInputView
    {
        public override void Init(SubscriptionObserver<float> rightMove, SubscriptionObserver<float> leftMove, float speed)
        {
            base.Init(rightMove, leftMove, speed);
            GameUpdater.Instance.Add(Move);
        }

        private void Move()
        {
            OnRightMove(Input.GetAxis("Horizontal"));
        }
    }
}