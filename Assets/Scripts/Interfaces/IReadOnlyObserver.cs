using System;

namespace Assets.Scripts.Interfaces
{
    public interface IReadOnlyObserver<T>
    {
        T Value { get; set; }

        void SubscribeObserver(Action<T> action);

        void UnSubscribeObserver(Action<T> action);
    }
}