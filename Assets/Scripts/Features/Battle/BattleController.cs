using Assets.Scripts.Controllers;
using Assets.Scripts.Enums;
using Assets.Scripts.Profile;
using UnityEngine;

namespace Assets.Scripts.Features.Battle
{
    public class BattleController : BaseController
    {
        private string _pathToPrefab = "Prefabs/Battle";
        private BattleView _view;
        private PlayerProfile _profile;
        
        
        public BattleController(PlayerProfile profile)
        {
            _profile = profile;
        }

        public void Init(Transform uiPlace, GameObject enemyCar, float speed)
        {
            _view = LoadView<BattleView>(_pathToPrefab);
            _view.Init(StartBattle, BackToMenu, enemyCar, speed);
        }

        private void StartBattle()
        {
            Debug.Log(nameof(StartBattle));
        }

        private void BackToMenu()
        {
            _profile.ObserverStateGame.Value = StateGame.Menu;
        }
    }
}