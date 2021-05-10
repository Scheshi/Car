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

        public DailyRewardsController(PlayerProfile profile)
        {
            _profile = profile;
        }
        
        public void Init(Transform uiPlace)
        {
            _view = LoadView<DailyRewardsView>(_pathToPrefab, uiPlace);
            _view.Init(AddReward, BackToMenu);
            _view.SetReward(DailyType.Coin, PlayerPrefs.GetInt(DailyType.Coin.ToString()));
            _view.SetReward(DailyType.Diamond, PlayerPrefs.GetInt(DailyType.Diamond.ToString()));
        }

        private void AddReward(DailyType type, int count)
        {
            var oldCount = PlayerPrefs.GetInt(type.ToString(), 0);
            PlayerPrefs.SetInt(type.ToString(), oldCount + count);
            _view.AddReward(type, count);
        }

        private void BackToMenu()
        {
            _profile.ObserverStateGame.Value = StateGame.Menu;
        }
    }
}