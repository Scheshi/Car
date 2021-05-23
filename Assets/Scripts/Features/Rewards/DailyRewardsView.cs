using System;
using Assets.Scripts.Configs.Tweeners;
using Assets.Scripts.Enums;
using DG.Tweening;
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
        [SerializeField] private Text _timeText;
        [SerializeField] private Button _resetTimeButton;
        [SerializeField] private RewardSlot[] _slots;
        [SerializeField] private AnimationTween _tween;
        private bool _isAnimate;
        private Sequence _sequence;

        public void Init(Action<DailyType, int> onReward, Action onBackToMenu, Action onResetTime)
        {
            _backToMenuButton.onClick.AddListener(onBackToMenu.Invoke);
            if (_content.childCount > 0)
            {
                _slots = _content.GetComponentsInChildren<RewardSlot>();
                foreach (var rewardButton in _slots)
                {
                    rewardButton.Init(onReward);
                }
            }
            _resetTimeButton.onClick.AddListener(onResetTime.Invoke);
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

        public void ShowTime(TimeSpan date, int currentSlot)
        {
            foreach (var slot in _slots)
            {
                slot.SetInteractable(false);
            }
            var canInteract = date <= TimeSpan.Zero;
            _timeText.text = !canInteract ? date.ToString("hh\\:mm\\:ss") : "Заберите свой приз!";
            if (currentSlot >= _slots.Length) currentSlot %= _slots.Length;
            _slots[currentSlot].SetInteractable(canInteract);
            if (canInteract && !_isAnimate)
            {
                _isAnimate = true;
                StartTween(_slots[currentSlot].transform, () =>
                {
                    _isAnimate = false;
                    _sequence = null;
                });
            }
        }

        private void StartTween(Transform currentTransform, Action onComplete)
        {
            _tween.DOAnimation(currentTransform, onComplete);
        }

        private void OnDestroy()
        {
            _backToMenuButton.onClick.RemoveAllListeners();
            _resetTimeButton.onClick.RemoveAllListeners();
        }
    }
}