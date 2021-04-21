using System.Collections.Generic;
using Assets.Scripts.Actions;
using Assets.Scripts.BackGround;
using Assets.Scripts.Enums;
using Assets.Scripts.GenerateLevel;
using Assets.Scripts.Inputer;
using Assets.Scripts.MainMenu;
using Assets.Scripts.Profile;
using UnityEngine;


namespace Assets.Scripts.Controllers
{
    public class MainController : BaseController
    {
        private BackgroundController _backgroundController;
        private InputController _input;
        private PlayerProfile _profile;
        private SubscriptionObserver<float> _rightMove;
        private SubscriptionObserver<float> _leftMove;
        private CarController _carController;

        public MainController(PlayerProfile profile, Transform placeUI)
        {
            _profile = profile;
            _carController = new CarController(_profile.Car);
            AddController(_carController);
            _leftMove = new SubscriptionObserver<float>();
            _rightMove = new SubscriptionObserver<float>();
            _input = new InputController(_profile.ObserverInput);
            AddController(_input);
            _profile.ObserverInput.SubscribeObserver(_input.ChangeCurrentInput);
            _profile.ObserverStateGame.SubscribeObserver(_carController.ChangeState);
            var menuController = new MainMenuController(placeUI, _profile);
            AddController(menuController);
            _profile.ObserverStateGame.SubscribeObserver(OnChangeValue);
        }
        
        private void OnChangeValue(StateGame state)
        {
            switch (state)
            {
                case StateGame.Game:
                    _profile.Analytic.SendMessage("start_game", new Dictionary<string, object>());
                    _backgroundController = new BackgroundController();
                    AddController(_backgroundController);
                    _rightMove.SubscribeObserver(_backgroundController.ChangeSpeed);
                    _rightMove.SubscribeObserver(_carController.Move);
                    _input.Init(_leftMove, _rightMove, _profile.Car);
                    var generateLevel =
                        new GenerateLevelController(new SubscriptionObserver<bool>(), _backgroundController);
                    AddController(generateLevel);
                    generateLevel.Init();
                

                    break;
                case StateGame.Menu:
                    _backgroundController?.Dispose();
                    _backgroundController = null;
                    _input.Dispose();
                    break;
            }
        }
        
    }
}