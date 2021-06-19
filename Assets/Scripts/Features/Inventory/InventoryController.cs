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


        public InventoryController(List<UsableItem> items)
        {
            _model = new InventoryModel(items);
        }

        public InventoryController(IInventoryModel model)
        {
            _model = model;
        }

        public void Init(Transform uiTransform)
        {
            _view = LoadView<InventoryView>(_pathToPrefab, uiTransform);
            _model.Items.SubscribeObserver(_view.Build);
            _view.Init(SetCurrentSelectedItem, UnsetCurrentSelectedItem);
            _view.Build(_model.Items.List);
        }

        public void Deinit()
        {
            _model.Items.UnSubscribeObserver(_view.Build);
            _view.Deinit();
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
            if (_view != null)
            {
                _model.Items.UnSubscribeObserver(_view.Build);
            }
            _model = null;
            base.Dispose();
        }
    }
}