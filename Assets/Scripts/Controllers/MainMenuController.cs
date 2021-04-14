using Assets.Scripts.Enums;
using Assets.Scripts.Profile;
using UnityEngine;
using Object = UnityEngine;

namespace Assets.Scripts.Interfaces
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
            _view.Init(StartGame);
        }

        private MainMenuView LoadView(Transform UITransform)
        {
            return UnityEngine.Object.
                Instantiate(Resources.Load<MainMenuView>(_pathToView), UITransform);
        }

        private void StartGame()
        {
            _profile.Observer.Value = StateGame.Game;
        }
    }
}