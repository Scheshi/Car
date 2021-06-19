using System;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Controllers
{
    public abstract class BaseController : IDisposable
    {
        private readonly List<BaseController> _controllers = new List<BaseController>();
        private readonly List<GameObject> _views = new List<GameObject>();

        public virtual void Dispose()
        {
            for (int i = 0; i < _controllers.Count; i++)
            {
                if(_controllers[i] != null)
                    _controllers[i].Dispose();
            }
            _controllers.Clear();

            for (int i = 0; i < _views.Count; i++)
            {
                if (_views[i] != null)
                    UnityEngine.Object.Destroy(_views[i].gameObject);
            }
            _views.Clear();
        }
        
        
        protected T LoadView<T>(string path) where T: Component
        {
            if (!String.IsNullOrEmpty(path))
            {
                var view = UnityEngine.Object.Instantiate(Resources.Load<T>(path));
                AddGameObject(view.gameObject);
                return view;
            }
            return null;
        }

        protected T LoadView<T>(string path, Transform parent) where T : Component
        {
            if (!String.IsNullOrEmpty(path))
            {
                var view = UnityEngine.Object.Instantiate(Resources.Load<T>(path), parent);
                AddGameObject(view.gameObject);
                return view;
            }
            return null;
        }

        protected void AddController(BaseController controller)
        {
            _controllers.Add(controller);
        }

        protected void AddGameObject(GameObject gameObject)
        {
            _views.Add(gameObject);
        }

        protected void RemoveGameObject(GameObject gameObject)
        {
            _views.Remove(gameObject);
        }
    }
}