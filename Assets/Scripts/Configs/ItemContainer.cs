using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Configs/Item Container")]
    public class ItemContainer : ScriptableObject
    {
        public List<UsableItem> Items;
    }
}