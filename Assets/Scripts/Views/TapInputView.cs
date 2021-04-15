using Assets.Scripts.Actions;
using UnityEngine;

namespace Assets.Scripts
{
    public class TapInputView : BaseInputView
    {
        private Camera _camera;
        public override void Init(SubscriptionObserver<float> rightMove, SubscriptionObserver<float> leftMove, float speed)
        {
            base.Init(rightMove, leftMove, speed);
            _camera = Camera.main;
            GameUpdater.Instance.Add(Move);
        }

        private void Move()
        {
            if(Input.touchCount > 0)
            {
                var touch = Input.touches[0];
                var center = _camera.rect.width / 2;
                if (touch.position.x > center)
                {
                    OnRightMove(1);
                }
                else if (touch.position.x < center)
                {
                    OnRightMove(-1);
                }
            }
        }
        
        private void OnDestroy()
        {
            GameUpdater.Instance.Remove(Move);
        }
    }
}