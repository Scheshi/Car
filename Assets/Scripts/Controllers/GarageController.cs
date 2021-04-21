using Assets.Scripts.Features.Garage;
using Assets.Scripts.Features.Inventory;


namespace Assets.Scripts.Controllers
{
    public class GarageController : BaseController, IUpgradableCarHandler
    {
        private IInventoryController _inventory;
        public GarageController(IInventoryController inventory)
        {
            _inventory = inventory;
            _inventory.Init();
        }


        public IUpgradableCar UpgradeCar(IUpgradableCar car)
        {
            car.Upgrade(_inventory.CurrentSelectedItem.UpgradeType, _inventory.CurrentSelectedItem.IncrementCount);
            return car;
        }
    }
}