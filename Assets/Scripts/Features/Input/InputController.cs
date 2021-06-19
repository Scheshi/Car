using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Actions;
using Assets.Scripts.Controllers;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Inputer
{
    public class InputController : BaseController
    {
        private BaseInputView _view;
        private SubscriptionObserver<InputChoose> _chooseObserver;

        private Dictionary<InputChoose, string> _switch = new Dictionary<InputChoose, string>()
        {
            {InputChoose.Keyboard, "Prefabs/Inputs/keyboardMove"},
            {InputChoose.Gyroscope, "Prefabs/Inputs/gyroscopeMove"},
            {InputChoose.Acceleration, "Prefabs/Inputs/accelerationMove"},
            {InputChoose.Tap, "Prefabs/Inputs/tapMove"},
            {InputChoose.Swipe, "Prefabs/Inputs/swipeMove"}
        };

        private InputChoose _currentInput = InputChoose.Gyroscope;

        public InputController(SubscriptionObserver<InputChoose> chooseObserver)
        {
            _chooseObserver = chooseObserver;
            _chooseObserver.SubscribeObserver(ChangeCurrentInput);
        }

        public void Init(SubscriptionObserver<float> leftMove, SubscriptionObserver<float> rightMove, Car car)
        {
            _view = LoadView<BaseInputView>(_switch[_currentInput]);
            AddGameObject(_view.gameObject);
            _view.Init(rightMove, leftMove, car.Speed);
        }

        public void ChangeCurrentInput(InputChoose input)
        {
            _currentInput = input;
        }
    }
}