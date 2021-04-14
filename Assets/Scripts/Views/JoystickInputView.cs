using Assets.Scripts.Actions;

namespace Assets.Scripts
{
    public class JoystickInputView : BaseInputView
    {
        public override void Init(SubscriptionObserver<float> rightMove, SubscriptionObserver<float> leftMove, float speed)
        {
            base.Init(rightMove, leftMove, speed);
            GameUpdater.Instance.Add(Move);
        }

        public void OnDestroy()
        {
            GameUpdater.Instance.Remove(Move);
        }

        private void Move()
        {
            
        }
    }
}