using System;
using System.Collections.Generic;
using Assets.Scripts.Configs;
using Assets.Scripts.Controllers;
using UnityEngine;
using Object = System.Object;

namespace Assets.Scripts.Features.Inventory
{
    public class InventoryController : BaseController, IInventoryController
    {
        private string _pathToPrefab = "Prefabs/Inventory";
        private IInventoryModel _model;
        private IInventoryView _view;
        public UsableItem CurrentSelectedItem { get; private set; }


        public InventoryController(List<UsableItem> items, Transform uiTransform)
        {
            _model = new InventoryModel(items);
            _view = LoadView(uiTransform);
            Hide();
        }

        private InventoryView LoadView(Transform uiTransform)
        {
            var view = UnityEngine.Object.Instantiate(Resources.Load<InventoryView>(_pathToPrefab), uiTransform);
            AddGameObject(view.gameObject);
            return view;
        }
        
        public InventoryController(IInventoryModel model, Transform uiTransform)
        {
            _model = model;
            SetPathToPrefab(_pathToPrefab);
            _view = LoadView(uiTransform);
            Hide();
        }

        public void Init()
        {
            _model.Items.SubscribeObserver(_view.Build);
            _view.Init(SetCurrentSelectedItem, UnsetCurrentSelectedItem);
            _view.Build(_model.Items.List);
            Show();
        }

        public void Deinit()
        {
            _model.Items.UnSubscribeObserver(_view.Build);
            _view.Deinit();
            Hide();
        }

        private void SetCurrentSelectedItem(UsableItem item) => CurrentSelectedItem = item;

        private void UnsetCurrentSelectedItem(UsableItem item)
        {
            if (CurrentSelectedItem == item)
            {
                CurrentSelectedItem = null;
            }
        }

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