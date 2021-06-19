using System;
using System.Collections.Generic;
using Assets.Scripts.Actions;
using Assets.Scripts.Configs;
using UnityEngine;

namespace Assets.Scripts.Features.Inventory
{
    public interface IInventoryView
    {
        void Init(Action<UsableItem> onSelected, Action<UsableItem> onDeselected);
        void Deinit();
        void Build(List<UsableItem> items);
        void Show();
        void Hide();
    }

    public interface IInventoryModel
    {
        SubscriptionListObserver<UsableItem> Items { get; }
        bool AddItem(UsableItem item);
        bool RemoveItem(UsableItem item);
    }

    public interface IInventoryController
    {
        UsableItem CurrentSelectedItem { get; }
        void Init(Transform ui);
        void Deinit();
    }
}