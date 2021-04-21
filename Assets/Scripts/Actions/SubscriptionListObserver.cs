using System;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Actions
{
    public class SubscriptionListObserver<T>: IReadOnlyListObserver<T>
    {

        private Action<List<T>> _action = delegate(List<T> obj) {  };
        private List<T> _list = new List<T>();

        public void Add(T item)
        {
            _list.Add(item);
            _action.Invoke(_list);
        }

        public bool Remove(T item)
        {
            var flag = _list.Remove(item);
            _action.Invoke(_list);
            return flag;
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void AddRange(List<T> items)
        {
            _list.AddRange(items);
            _action.Invoke(_list);
        }

        public List<T> List => _list;

        public void SubscribeObserver(Action<List<T>> action)
        {
            _action += action;
        }

        public void UnSubscribeObserver(Action<List<T>> action)
        {
            _action -= action;
        }
    }
}