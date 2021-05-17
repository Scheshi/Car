using System;
using System.Globalization;
using Assets.Scripts.Controllers;
using Assets.Scripts.Enums;
using Assets.Scripts.Profile;
using UnityEngine;


namespace Assets.Scripts.Features.Rewards
{
    public class DailyRewardsController : BaseController
    {
        private string _pathToPrefab = "Prefabs/Rewards";
        private DailyRewardsView _view;
        private PlayerProfile _profile;
        private DateTime _nextRewardTime;
        private int _rewardCount;

        public DailyRewardsController(PlayerProfile profile)
        {
            _profile = profile;
        }
        
        public void Init(Transform uiPlace)
        {
            if(!DateTime.TryParse(PlayerPrefs.GetString(nameof(_nextRewardTime)), CultureInfo.CurrentCulture, DateTimeStyles.None, out _nextRewardTime))
            {
                _nextRewardTime = DateTime.UtcNow;
            }

            _rewardCount = PlayerPrefs.GetInt(nameof(_rewardCount), 0);
            _view = LoadView<DailyRewardsView>(_pathToPrefab, uiPlace);
            _view.Init(AddReward, BackToMenu, ResetTime);
            _view.SetReward(DailyType.Coin, PlayerPrefs.GetInt(DailyType.Coin.ToString()));
            _view.SetReward(DailyType.Diamond, PlayerPrefs.GetInt(DailyType.Diamond.ToString()));
            GameUpdater.Instance.Add(TimeCheck);
        }

        private void AddReward(DailyType type, int count)
        {
            var oldCount = PlayerPrefs.GetInt(type.ToString(), 0);
            PlayerPrefs.SetInt(type.ToString(), oldCount + count);
            _nextRewardTime = DateTime.UtcNow.Add(new TimeSpan(1, 0, 0, 0));
            PlayerPrefs.SetString(nameof(_nextRewardTime), _nextRewardTime.ToString());
            _rewardCount++;
            PlayerPrefs.SetInt(nameof(_rewardCount), _rewardCount);
            _view.AddReward(type, count);
        }

        private void BackToMenu()
        {
            _profile.ObserverStateGame.Value = StateGame.Menu;
        }

        private void TimeCheck()
        {
            _view.ShowTime(_nextRewardTime.Subtract(DateTime.UtcNow), _rewardCount);
        }

        public override void Dispose()
        {
            GameUpdater.Instance.Remove(TimeCheck);
            base.Dispose();
        }

        private void ResetTime()
        {
            _nextRewardTime = DateTime.UtcNow;
        }
    }
}