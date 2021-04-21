using Assets.Scripts.Features.Inventory;

namespace Assets.Scripts.Controllers
{
    public class GarageController : BaseController
    {
        private IInventoryController _inventory;
        public GarageController(IInventoryController inventory)
        {
            _inventory = inventory;
        }


    }
}