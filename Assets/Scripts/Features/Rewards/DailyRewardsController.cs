using Assets.Scripts.Controllers;
using UnityEngine;

namespace Assets.Scripts.Features.Rewards
{
    public class DailyRewardsController : BaseController
    {
        private string _pathToPrefab;
        private DailyRewardsView _view;

        public void Init(Transform uiPlace)
        {
            _view = LoadView<DailyRewardsView>(_pathToPrefab, uiPlace);
        }
    }
}