using Assets.Scripts.Actions;
using UnityEngine;

namespace Assets.Scripts
{
    public class SwipeInputView : BaseInputView
    {
        private Vector2 _beginTouchPosition = Vector2.zero;
        private Vector2 _currentTouchPosition = Vector2.zero;
        private Camera _camera;
        
        
        public override void Init(SubscriptionObserver<float> rightMove, SubscriptionObserver<float> leftMove, float speed)
        {
            base.Init(rightMove, leftMove, speed);
            GameUpdater.Instance.Add(Move);
            _camera = Camera.main;
        }

        private void Move()
        {
            if(Input.touchCount > 0)
            {
                var touch = Input.touches[0];
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _beginTouchPosition = _camera.ScreenToWorldPoint(touch.position);
                        break;
                    case TouchPhase.Moved:
                        _currentTouchPosition = _camera.ScreenToWorldPoint(touch.position);
                        break;
                    case TouchPhase.Stationary:
                        _currentTouchPosition = _camera.ScreenToWorldPoint(touch.position);
                        break;
                }
                var direction = _currentTouchPosition - _beginTouchPosition;
                if(direction.x > 1) direction.Normalize();
                
                OnRightMove(direction.x);
            }
        }

        private void OnDestroy()
        {
            GameUpdater.Instance.Remove(Move);
        }
    }
}