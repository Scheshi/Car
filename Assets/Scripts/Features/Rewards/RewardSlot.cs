using System;
using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Features.Rewards
{
    [RequireComponent(typeof(Button))]
    public class RewardSlot : MonoBehaviour
    {
        [SerializeField]
        private DailyType _reward;
        [SerializeField] private int _count;
        [SerializeField] private Button _button;

        private void Start()
        {
            if (_button == null) _button = GetComponent<Button>();
        }
        
        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void Init(Action<DailyType, int> onReward)
        {
            _button.onClick.AddListener(() => onReward.Invoke(_reward, _count));
        }

        public void SetInteractable(bool isInteractable)
        {
            _button.interactable = isInteractable;
        }
    }
}