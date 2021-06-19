using Assets.Scripts.Actions;
using UnityEngine;

namespace Assets.Scripts.Inputer.Views
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
                var center = _camera.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2)).x;
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_camera.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2)), 1);
        }
    }
}