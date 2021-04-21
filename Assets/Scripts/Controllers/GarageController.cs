using Assets.Scripts.Features.Inventory;

namespace Assets.Scripts.Controllers
{
    public class GarageController : BaseController
    {
        private InventoryController _inventory;
        public GarageController(InventoryController inventory)
        {
            _inventory = inventory;
            AddController(inventory);
        }


    }
}