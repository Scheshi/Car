using Assets.Scripts.Enums;
using Assets.Scripts.Features.Garage;
using Assets.Scripts.Features.Inventory;
using Assets.Scripts.Profile;
using UnityEngine;


namespace Assets.Scripts.Controllers
{
    public class GarageController : BaseController, IUpgradableCarHandler
    {
        private string _pathToPrefab = "Prefabs/garage";
        private GarageView _view;
        
        private IInventoryController _inventory;
        private IUpgradableCar _currentCar;
        public GarageController(IInventoryController inventory)
        {
            _inventory = inventory;
            if(inventory is BaseController controller) AddController(controller);
        }

        public void Init(PlayerProfile profile, Transform placeUI)
        {
            _inventory.Init(placeUI);
            _view = LoadView<GarageView>(_pathToPrefab, placeUI);
            _view.Init(() => UpgradeCar(profile.Car), () =>
            {
                profile.ObserverStateGame.Value = StateGame.Menu;
            });
            _view.gameObject.SetActive(true);
        }

        public IUpgradableCar UpgradeCar(IUpgradableCar car)
        {
            car.Upgrade(_inventory.CurrentSelectedItem.UpgradeType, _inventory.CurrentSelectedItem.IncrementCount);
            _view.SetSlot(_inventory.CurrentSelectedItem);
            return car;
        }
    }
}