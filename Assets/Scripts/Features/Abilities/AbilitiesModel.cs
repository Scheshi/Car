using System.Collections.Generic;
using Assets.Scripts.Configs;
using UnityEngine;

namespace Assets.Scripts.Features.Abilities
{
    
    public class AbilitiesModel
    {
        private List<Ability> _abilities;
        public List<Ability> Abilities => _abilities;

        public AbilitiesModel(List<Ability> list)
        {
            _abilities = list;
        }

        public void ApplyAbility(int index, Transform transform)
        {
            _abilities[index].Apply(transform.position);
        }
    }
}