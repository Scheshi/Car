using System;
using System.Collections;
using System.Collections.Generic;
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

    public class SubscriptionObserverList<T> : IList<T>, IReadOnlyList<T>
    {
        private IList<T> _list;
        private Action<T> _onAddAction = obj => { };
        private Action<T> _onRemoveAction = obj => { };
        private Action _onClearAction = () => { };

        public SubscriptionObserverList()
        {
            _list = new List<T>();
        }

        public SubscriptionObserverList(List<T> enumerable)
        {
            _list = enumerable;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            _list.Add(item);
            _onAddAction.Invoke(item);
        }

        public void Clear()
        {
            _list.Clear();
            _onClearAction.Invoke();
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            _onRemoveAction.Invoke(item);
            return _list.Remove(item);
        }

        int ICollection<T>.Count => _list.Count;

        public bool IsReadOnly { get; }
        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            T item = _list[index];
            _list.RemoveAt(index);
            _onRemoveAction.Invoke(item);
        }

        public T this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }

        int IReadOnlyCollection<T>.Count => _list.Count;
    }
}