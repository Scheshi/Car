using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Configs/Item")]
    public class UsableItem : Item
    {
        public UpgradableType UpgradeType;
        public int IncrementCount;
    }
}