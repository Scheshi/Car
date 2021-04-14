using System;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Interfaces
{
    public abstract class BaseController : IDisposable
    {
        protected List<BaseController> _controllers = new List<BaseController>();
        protected List<GameObject> _views = new List<GameObject>();
        public virtual void Dispose()
        {
            for (int i = 0; i < _controllers.Count; i++)
            {
                _controllers[i].Dispose();
            }
            _controllers.Clear();

            for (int i = 0; i < _views.Count; i++)
            {
                UnityEngine.Object.Destroy(_views[i]);
            }
            _views.Clear();
        }

        protected void AddController(BaseController controller)
        {
            _controllers.Add(controller);
        }

        protected void AddGameObject(GameObject gameObject)
        {
            _views.Add(gameObject);
        }
    }
}