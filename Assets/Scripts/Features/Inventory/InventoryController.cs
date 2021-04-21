using System.Collections.Generic;
using Assets.Scripts.Configs;
using Assets.Scripts.Controllers;
using UnityEngine;
using Object = System.Object;

namespace Assets.Scripts.Features.Inventory
{
    public class InventoryController : BaseController
    {
        private string _pathToPrefab = "Prefabs/Inventory";
        private InventoryModel _model;
        private InventoryView _view;
        
        
        public InventoryController(List<UsableItem> items, Transform uiTransform)
        {
            _model = new InventoryModel(items);
            _view = LoadView(uiTransform);
            _view.Init(() =>
            {
                if (_view.IsHide)
                {
                    Show();
                }
                else Hide();
            });
            _view.Build(_model.Items.List);
            Hide();
        }

        private InventoryView LoadView(Transform uiTransform)
        {
            var view = UnityEngine.Object.Instantiate(Resources.Load<InventoryView>(_pathToPrefab), uiTransform);
            AddGameObject(view.gameObject);
            return view;
        }
        
        public InventoryController(InventoryModel model, Transform uiTransform)
        {
            _model = model;
            SetPathToPrefab(_pathToPrefab);
            _view = LoadView(uiTransform);
            _view.Build(_model.Items.List);
            Hide();
        }

        public void Init() => _model.Items.SubscribeObserver(_view.Build);

        public void Show() => _view.Show();

        public void Hide() => _view.Hide();

        public override void Dispose()
        {
            _model.Items.UnSubscribeObserver(_view.Build);
            _model = null;
            base.Dispose();
        }
    }
}