using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Configs/Ability Container")]
    public class AbilityContainer : ScriptableObject
    {
        public List<Ability> Abilities;
    }
}