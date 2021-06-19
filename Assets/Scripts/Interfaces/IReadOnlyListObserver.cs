using System;
using System.Collections.Generic;

namespace Assets.Scripts.Interfaces
{
    public interface IReadOnlyListObserver<T>
    {
        public List<T> List { get;}


        public void SubscribeObserver(Action<List<T>> action);

        public void UnSubscribeObserver(Action<List<T>> action);
    }
}