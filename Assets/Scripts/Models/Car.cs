using System;
using Assets.Scripts.Actions;

namespace Assets.Scripts
{
    public class Car
    {
        public float Speed { get; }

        public Car(float speed)
        {
            Speed = speed;
        }

        public float Moving(float rightMove)
        {
            if (rightMove > 0) return Speed; 
            if (rightMove < 0) return -Speed;
            return 0;
        }
    }
}