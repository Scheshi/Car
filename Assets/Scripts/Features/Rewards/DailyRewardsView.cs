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
        [SerializeField] private Text _coinsCount;
        [SerializeField] private Text _diamondsCount;

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

        public void AddReward(DailyType dailyType, int count)
        {
            switch (dailyType)
            {
                case DailyType.Coin:
                    _coinsCount.text = (int.Parse(_coinsCount.text) + count).ToString();
                    break;
                case DailyType.Diamond:
                    _diamondsCount.text = (int.Parse(_diamondsCount.text) + count).ToString();
                    break;
            }
        }

        public void SetReward(DailyType dailyType, int count)
        {
            switch (dailyType)
            {
                case DailyType.Coin:
                    _coinsCount.text = count.ToString();
                    break;
                case DailyType.Diamond:
                    _diamondsCount.text = count.ToString();
                    break;
            }
        }

        private void OnDestroy()
        {
            _backToMenuButton.onClick.RemoveAllListeners();
        }
    }
}