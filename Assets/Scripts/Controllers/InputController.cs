using UnityEngine;
using Assets.Scripts.Actions;

namespace Assets.Scripts.Interfaces
{
    public class InputController : BaseController
    {
        private string _pathToInputView = "Prefabs/Inputs/keyboardMove";
        private BaseInputView _view;


        public InputController(SubscriptionObserver<float> leftMove, SubscriptionObserver<float> rightMove, Car car)
        {
            _view = LoadView();
            _view.Init(rightMove, leftMove, car.Speed);
        }

        private BaseInputView LoadView()
        {
            var obj = Object.Instantiate(Resources.Load<BaseInputView>(_pathToInputView));
            AddGameObject(obj.gameObject);
            return obj;
        }
    }
}