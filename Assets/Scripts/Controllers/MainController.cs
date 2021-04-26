using System.Collections.Generic;
using Assets.Scripts.Actions;
using Assets.Scripts.BackGround;
using Assets.Scripts.Configs;
using Assets.Scripts.Enums;
using Assets.Scripts.Features.Abilities;
using Assets.Scripts.Features.Garage;
using Assets.Scripts.Features.Inventory;
using Assets.Scripts.GenerateLevel;
using Assets.Scripts.Inputer;
using Assets.Scripts.MainMenu;
using Assets.Scripts.Profile;
using UnityEngine;


namespace Assets.Scripts.Controllers
{
    public class MainController : BaseController
    {
        private readonly PlayerProfile _profile;
        private BackgroundController _backgroundController;
        private InputController _input;
        private SubscriptionObserver<float> _rightMove;
        private SubscriptionObserver<float> _leftMove;
        private CarController _carController;
        private GenerateLevelController _generateLevel;
        private GarageController _garage;
        private AbilitiesController _abilities;
        
        public MainController(PlayerProfile profile, Transform placeUi)
        {
            _profile = profile;
            Init(profile, placeUi);
        }

        private void Init(PlayerProfile profile, Transform placeUi)
        {
            _carController = CarConstruct(profile);
            _input = InputConstruct(profile);
            _garage = GarageConstruct(placeUi, _profile.Car);
            var menuController = MenuConstruct(profile, placeUi);
            _profile.ObserverStateGame.SubscribeObserver(OnChangeValue);
            _backgroundController = BackgroundConstruct();
            _generateLevel = GenerateLevelConstruct();
            _abilities = AbilitiesConstruct(placeUi);
        }
        
        private void OnChangeValue(StateGame state)
        {
            switch (state)
            {
                case StateGame.Game:
                    _profile.Analytic.SendMessage("start_game", new Dictionary<string, object>());
                    _backgroundController.Init();
                    _rightMove.SubscribeObserver(_backgroundController.ChangeSpeed);
                    _rightMove.SubscribeObserver(_carController.Move);
                    _input.Init(_leftMove, _rightMove, _profile.Car);
                    _generateLevel.Init();
                    _garage.Deinit();
                    _garage.Dispose();
                    _abilities.Init();
                    break;
                case StateGame.Menu:
                    _backgroundController?.Dispose();
                    _input.Dispose();
                    _generateLevel.Dispose();
                    break;
                case StateGame.Garage:
                    _backgroundController?.Dispose();
                    _input.Dispose();
                    _generateLevel.Dispose();
                    _garage.Init();
                    break;
            }
        }

        #region Generates Depencity
        
        private AbilitiesController AbilitiesConstruct(Transform ui)
        {
           var controller = new AbilitiesController(Resources.Load<AbilityContainer>("Configs/Abilities"), ui);
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

        private MainMenuController MenuConstruct(PlayerProfile profile, Transform ui)
        {
            var menuController = new MainMenuController(ui, profile);
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
            profile.ObserverInput.SubscribeObserver(_input.ChangeCurrentInput);
            return input;
        }
        
        private GarageController GarageConstruct(Transform placeUI, IUpgradableCar car)
        {
            var itemContainer = Resources.Load<ItemContainer>("Configs/TestContainer");
            var inventory = new InventoryController(itemContainer.Items, placeUI);

            return new GarageController(inventory, placeUI, car);
        }
        
        #endregion
        
    }
}