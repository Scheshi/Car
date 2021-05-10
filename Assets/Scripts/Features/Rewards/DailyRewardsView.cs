using System;
using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Features.Rewards
{
    public class DailyRewardsView : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private Button _backToMenuButton;

        public void Init(Action<DailyType, int> onReward, Action onBackToMenu)
        {
            _backToMenuButton.onClick.AddListener(onBackToMenu.Invoke);
            if (_content.childCount > 0)
            {
                foreach (var rewardButton in _content.GetComponentsInChildren<RewardSlot>())
                {
                    rewardButton.Init(onReward);
                }
            }
        }

        private void OnDestroy()
        {
            _backToMenuButton.onClick.RemoveAllListeners();
        }
    }
}