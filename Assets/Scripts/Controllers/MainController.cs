using System.Collections.Generic;
using Assets.Scripts.Actions;
using Assets.Scripts.BackGround;
using Assets.Scripts.Configs;
using Assets.Scripts.Enums;
using Assets.Scripts.Features.Abilities;
using Assets.Scripts.Features.Inventory;
using Assets.Scripts.GenerateLevel;
using Assets.Scripts.Inputer;
using Assets.Scripts.MainMenu;
using Assets.Scripts.Profile;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.Controllers
{
    public class MainController : BaseController
    {
        private string _pathToButton = "Prefabs/BacktoMenuButton";
        private readonly PlayerProfile _profile;
        private BackgroundController _backgroundController;
        private InputController _input;
        private SubscriptionObserver<float> _rightMove;
        private SubscriptionObserver<float> _leftMove;
        private CarController _carController;
        private GenerateLevelController _generateLevel;
        private GarageController _garage;
        private AbilitiesController _abilities;
        private MainMenuController _menu;
        private Transform _placeUI;
        private Button _backToMenuButton;
        
        public MainController(PlayerProfile profile, Transform placeUi)
        {
            _profile = profile;
            _placeUI = placeUi;
            Init(profile);
        }

        private void Init(PlayerProfile profile)
        {
            _rightMove = new SubscriptionObserver<float>();
            _leftMove = new SubscriptionObserver<float>();
            _carController = CarConstruct(profile);
            _rightMove.SubscribeObserver(_carController.Move);
            profile.ObserverStateGame.SubscribeObserver(OnChangeValue);
            profile.ObserverStateGame.Value = StateGame.Menu;
        }
        
        private void OnChangeValue(StateGame state)
        {
            switch (state)
            {
                case StateGame.Game:
                    _profile.Analytic.SendMessage("start_game", new Dictionary<string, object>());
                    _backgroundController = BackgroundConstruct();
                    _backgroundController.Init();
                    _rightMove.SubscribeObserver(_backgroundController.ChangeSpeed);
                    _input = InputConstruct(_profile);
                    _input.Init(_leftMove, _rightMove, _profile.Car);
                    _generateLevel = GenerateLevelConstruct();
                    _generateLevel.Init();
                    _garage?.Dispose();
                    _abilities = AbilitiesConstruct(_placeUI);
                    _abilities.Init(_placeUI);
                    _menu?.Dispose();
                    _backToMenuButton = Object.Instantiate(Resources.Load<Button>(_pathToButton), _placeUI);
                    _backToMenuButton.onClick.AddListener(() =>
                    {
                        _profile.ObserverStateGame.Value = StateGame.Menu;
                    });
                    break;
                case StateGame.Menu:
                    _menu = MenuConstruct(_profile);
                    _menu.Init(_placeUI);
                    _backgroundController?.Dispose();
                    _input?.Dispose();
                    _generateLevel?.Dispose();
                    _garage?.Dispose();
                    _abilities?.Dispose();
                    if (_backToMenuButton != null)
                    {
                        _backToMenuButton.onClick.RemoveAllListeners();
                        Object.Destroy(_backToMenuButton.gameObject);
                    }
                    break;
                case StateGame.Garage:
                    _backgroundController?.Dispose();
                    _input?.Dispose();
                    _generateLevel?.Dispose();
                    _garage = GarageConstruct();
                    _garage.Init(_profile, _placeUI);
                    _menu?.Dispose();
                    break;
            }
        }

        #region Generates Depencity
        
        private AbilitiesController AbilitiesConstruct(Transform ui)
        {
           var controller = new AbilitiesController(Resources.Load<AbilityContainer>("Configs/Abilities"));
            AddController(controller);
            return controller;
        }
        
        private GenerateLevelController GenerateLevelConstruct()
        {
            var controller = new GenerateLevelController(new SubscriptionObserver<bool>(), _backgroundController);
            AddController(_generateLevel);
            return controller;
        }
        
        private BackgroundController BackgroundConstruct()
        {
            var controller = new BackgroundController();
            AddController(_backgroundController);
            return controller;
        }

        private MainMenuController MenuConstruct(PlayerProfile profile)
        {
            var menuController = new MainMenuController(profile);
            AddController(menuController);
            return menuController;
        }
        
        private CarController CarConstruct(PlayerProfile profile)
        {
            var controller = new CarController(profile.Car);
            AddController(controller);
            profile.ObserverStateGame.SubscribeObserver(controller.ChangeState);
            return controller;
        }

        private InputController InputConstruct(PlayerProfile profile)
        {
            _leftMove = new SubscriptionObserver<float>();
            _rightMove = new SubscriptionObserver<float>();
            var input = new InputController(profile.ObserverInput);
            AddController(input);
            profile.ObserverInput.SubscribeObserver(input.ChangeCurrentInput);
            return input;
        }
        
        private GarageController GarageConstruct()
        {
            var itemContainer = Resources.Load<ItemContainer>("Configs/TestContainer");
            var inventory = new InventoryController(itemContainer.Items);

            return new GarageController(inventory);
        }
        
        #endregion
        
    }
}