using System;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;


namespace Assets.Scripts.Actions
{
    public class SubscriptionObserver<T>: IReadOnlyObserver<T>
    {
        private Action<T> _action = delegate(T obj) {  };
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                _action?.Invoke(_value);
            }
        }

        public void SubscribeObserver(Action<T> action)
        {
            _action += action;
        }

        public void UnSubscribeObserver(Action<T> action)
        {
            _action -= action;
        }
    }
}