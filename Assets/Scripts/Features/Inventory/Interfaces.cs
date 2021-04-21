using System;
using System.Collections.Generic;
using Assets.Scripts.Actions;
using Assets.Scripts.Configs;

namespace Assets.Scripts.Features.Inventory
{
    public interface IInventoryView
    {
        bool IsHide { get; }
        void Init(Action action);
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
        void Init();
        void Show();
        void Hide();
    }
}