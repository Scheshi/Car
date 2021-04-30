using System;
using System.Collections.Generic;
using Assets.Scripts.Configs;
using Assets.Scripts.Services;
using UnityEngine;

namespace Assets.Scripts.Features.Abilities
{
    public class AbilitiesView : MonoBehaviour
    {
        [SerializeField] private Slot _slotPrefab;
        [SerializeField] private Transform _content;
        private List<Slot> _currentAbilitiesSlot;

        public void Init(List<Ability> abilities, Action<int, Transform> action)
        {
            _currentAbilitiesSlot = BuildMenu(abilities.Count);
            if (_currentAbilitiesSlot.Count > 0)
            {
                for (int i = 0; i < _currentAbilitiesSlot.Count; i++)
                {
                    var i1 = i;
                    if (_currentAbilitiesSlot[i] == null) Debug.Log("current ability is null");
                    if (abilities[i].Sprite == null) Debug.Log("sprite is null");
                    _currentAbilitiesSlot[i].SetSlot(abilities[i].Sprite, abilities[i].Name, () =>
                    {
                        action.Invoke(i1, transform);
                        _currentAbilitiesSlot[i1].SetInteractiveButton(false);
                    });
                }
            }
        }

        private List<Slot> BuildMenu(int count)
        {
            if (_content.childCount > count)
            {
                for (int i = count; i < _content.childCount; i++)
                {
                    Destroy(_content.GetChild(i).gameObject);
                }
            }
            else if (count > _content.childCount)
            {
                for (int i = _content.childCount; i < count; i++)
                {
                    Instantiate(_slotPrefab.gameObject, _content);
                }
            }

            var list = new List<Slot>();
            for (int i = 0; i < count; i++)
            {
                list.Add(_content.GetChild(i).GetComponent<Slot>());
            }

            return list;
        }

        private void OnDestroy()
        {
            if (_currentAbilitiesSlot != null && _currentAbilitiesSlot.Count > 0)
            {
                for (int i = 0; i < _currentAbilitiesSlot.Count; i++)
                {
                    Destroy(_currentAbilitiesSlot[i].gameObject);
                    i--;
                }
            }
        }
    }
}