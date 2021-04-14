using Assets.Scripts.Actions;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Profile
{
    public class PlayerProfile
    {
        public Car Car { get; }
        public SubscriptionObserver<StateGame> Observer { get; }

        public PlayerProfile(float speedCar)
        {
            Car = new Car(speedCar);
            Observer = new SubscriptionObserver<StateGame>();
            //Observer.Value = StateGame.Menu;
        }
    }
}