using Assets.Scripts.Actions;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Profile
{
    public class PlayerProfile
    {
        public Car Car { get; }
        public SubscriptionObserver<StateGame> ObserverStateGame { get; }

        public SubscriptionObserver<InputChoose> ObserverInput { get; }
        
        public SubscriptionObserver<int> WinCount { get; }
        
        public IAnalytic Analytic { get; }

        public PlayerProfile(float speedCar, IAnalytic analytic)
        {
            WinCount = new SubscriptionObserver<int>();
            WinCount.Value = 0;
            Car = new Car(speedCar);
            ObserverStateGame = new SubscriptionObserver<StateGame>();
            ObserverInput = new SubscriptionObserver<InputChoose>();
            Analytic = analytic;
        }
    }
}