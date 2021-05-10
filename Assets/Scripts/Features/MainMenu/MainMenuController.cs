using Assets.Scripts.Controllers;
using Assets.Scripts.Enums;
using Assets.Scripts.Profile;
using UnityEngine;
using Object = UnityEngine;

namespace Assets.Scripts.MainMenu
{
    public class MainMenuController : BaseController
    {
        private string _pathToView = "Prefabs/mainMenu";
        private MainMenuView _view;
        private PlayerProfile _profile;

        public MainMenuController(PlayerProfile playerProfile)
        {
            _profile = playerProfile;
        }
        
        public void Init(Transform UITransform)
        {
            _view = LoadView<MainMenuView>(_pathToView, UITransform);
            _view.Init(StartGame, OpenGarage, ChooseInput, StartBattle, DailyRewards);
        }

        private void StartBattle()
        {
            _profile.ObserverStateGame.Value = StateGame.Battle;
        }

        private void StartGame()
        {
            _profile.ObserverStateGame.Value = StateGame.Game;
        }

        private void ChooseInput(int value)
        {
            _profile.ObserverInput.Value = (InputChoose) value;
        }

        private void OpenGarage()
        {
            _profile.ObserverStateGame.Value = StateGame.Garage;
        }

        private void DailyRewards()
        {
            _profile.ObserverStateGame.Value = StateGame.DailyRewards;
        }
    }
}