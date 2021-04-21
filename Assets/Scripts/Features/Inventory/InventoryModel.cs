using System.Collections.Generic;
using Assets.Scripts.Actions;
using Assets.Scripts.Configs;

namespace Assets.Scripts.Features.Inventory
{
    public class InventoryModel
    {
        public SubscriptionListObserver<UsableItem> Items { get; }
        public InventoryModel(List<UsableItem> items)
        {
            Items = new SubscriptionListObserver<UsableItem>();
            if(items != null && items.Count > 0)
                Items.AddRange(items);
        }

        public bool AddItem(UsableItem item)
        {
            if (Items.Contains(item)) return false;
            Items.Add(item);
            return true;
        }

        public bool RemoveItem(UsableItem item)
        {
            return Items.Remove(item);
        }
    }
}