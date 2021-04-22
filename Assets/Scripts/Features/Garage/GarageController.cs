using Assets.Scripts.Features.Garage;
using Assets.Scripts.Features.Inventory;
using UnityEngine;


namespace Assets.Scripts.Controllers
{
    public class GarageController : BaseController, IUpgradableCarHandler
    {
        private string _pathToPrefab = "Prefabs/garage";
        private GarageView _view;
        
        private IInventoryController _inventory;
        private IUpgradableCar _currentCar;
        public GarageController(IInventoryController inventory, Transform placeUI, IUpgradableCar car)
        {
            _inventory = inventory;
            _view = LoadView(placeUI);
            _view.Init(() => UpgradeCar(car));
            _view.gameObject.SetActive(false);
        }

        private GarageView LoadView(Transform placeUI)
        {
            return Object.Instantiate(Resources.Load<GarageView>(_pathToPrefab), placeUI);
        }
        
        

        public void Init()
        {
            _inventory.Init();
            _view.gameObject.SetActive(true);
        }

        public void Deinit()
        {
            _inventory.Deinit();
            _view.gameObject.SetActive(false);
        }


        public IUpgradableCar CurrentCar => _currentCar;

        public IUpgradableCar UpgradeCar(IUpgradableCar car)
        {
            car.Upgrade(_inventory.CurrentSelectedItem.UpgradeType, _inventory.CurrentSelectedItem.IncrementCount);
            _view.SetSlot(_inventory.CurrentSelectedItem);
            return car;
        }
    }
}