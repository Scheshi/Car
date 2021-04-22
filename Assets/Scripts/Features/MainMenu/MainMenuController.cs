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

        public MainMenuController(Transform UITransform, PlayerProfile playerProfile)
        {
            _profile = playerProfile;
            _view = LoadView(UITransform);
            _view.Init(StartGame, OpenGarage, ChooseInput);
        }

        private MainMenuView LoadView(Transform UITransform)
        {
            return UnityEngine.Object.
                Instantiate(Resources.Load<MainMenuView>(_pathToView), UITransform);
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
    }
}