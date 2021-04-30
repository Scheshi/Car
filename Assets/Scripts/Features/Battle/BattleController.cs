using System.Collections.Generic;
using Assets.Scripts.Controllers;
using Assets.Scripts.Enums;
using Assets.Scripts.Profile;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Features.Battle
{
    public class BattleController : BaseController
    {
        private string _pathToPrefab = "Prefabs/Battle";
        private BattleView _view;
        private PlayerProfile _profile;

        private List<Sprite> _enemies;
        
        
        public BattleController(PlayerProfile profile)
        {
            _enemies = new List<Sprite>()
            {
                Resources.Load<Sprite>("Car/TruckChassisSprite")
            };
            _profile = profile;
        }

        public void Init(Transform uiPlace, float speed)
        {
            _view = LoadView<BattleView>(_pathToPrefab, uiPlace);
            _view.Init(StartBattle, BackToMenu, _enemies[0], speed);
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