using System;
using Assets.Scripts.Actions;
using Assets.Scripts.Enums;
using Assets.Scripts.Features.Garage;
using UnityEngine;

namespace Assets.Scripts
{
    public class Car : IUpgradableCar
    {
        private float _defaultSpeed;
        public float Speed { get; private set; }

        public SubscriptionObserver<float> CurrentSpeedObserver { get; private set; } =
            new SubscriptionObserver<float>();
        
        public void Restore()
        {
            Speed = _defaultSpeed;
        }

        public void Upgrade(UpgradableType type, float incrementPoint)
        {
            switch (type)
            {
               case UpgradableType.Speed:
                   Restore();
                   Speed += incrementPoint;
                   break;
            }
        }

        public Car(float speed)
        {
            _defaultSpeed = speed;
            Restore();
        }

        public float Moving(float rightMove)
        {
            if (rightMove > 0) CurrentSpeedObserver.Value = Speed; 
            else if (rightMove < 0) CurrentSpeedObserver.Value = -Speed;
            else CurrentSpeedObserver.Value = 0;
            return CurrentSpeedObserver.Value;
        }
    }
}