using Assets.Scripts.Enums;


namespace Assets.Scripts.Controllers
{
    public class CarController : BaseController
    {
        private string _pathToPrefab = "Prefabs/car";
        private Car _model;
        private CarView _view;

        public CarController(Car model)
        {
            _model = model;
        }

        public void Move(float value)
        {
            _view.ChangeSpeed(_model.Moving(value));
        }

        public void ChangeState(StateGame state)
        {
            switch (state)
            {
                case StateGame.Menu:
                    Dispose();
                    break;
                case StateGame.Game:
                    _view = LoadView<CarView>(_pathToPrefab);
                    break;
            }
        }
        
        
        
        
    }
}