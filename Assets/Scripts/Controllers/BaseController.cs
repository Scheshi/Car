using System;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Controllers
{
    public abstract class BaseController : IDisposable
    {
        private readonly List<BaseController> _controllers = new List<BaseController>();
        private readonly List<GameObject> _views = new List<GameObject>();
        private string _pathToPrefab = String.Empty;
        
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

        protected void SetPathToPrefab(string path)
        {
            _pathToPrefab = path;
        }
        
        protected T LoadView<T>() where T: Component
        {
            if (!String.IsNullOrEmpty(_pathToPrefab))
            {
                var view = UnityEngine.Object.Instantiate(Resources.Load<T>(_pathToPrefab));
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