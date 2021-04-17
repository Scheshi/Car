using Assets.Scripts;
using Assets.Scripts.Actions;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Profile;
using UnityEngine;


public class GameStarter : MonoBehaviour
{
    [SerializeField] private Transform _placeUI;
    [SerializeField] private float _speedCar;
    [SerializeField] private GenerateLevelView _generateLevel;
    private BackgroundController _backgroundController;
    private InputController _input;
    private PlayerProfile _profile;
    private SubscriptionObserver<float> _rightMove;
    private SubscriptionObserver<float> _leftMove;
    private CarController _carController;
    void Start()
    {
        new GameObject("Updater").AddComponent<GameUpdater>();
        _profile = new PlayerProfile(_speedCar);
        _carController = new CarController(_profile.Car);
        _leftMove = new SubscriptionObserver<float>();
        _rightMove = new SubscriptionObserver<float>();
        _input = new InputController(_profile.ObserverInput);
        _profile.ObserverInput.SubscribeObserver(_input.ChangeCurrentInput);
        _profile.ObserverStateGame.SubscribeObserver(_carController.ChangeState);
        var menuController = new MainMenuController(_placeUI, _profile);
        _profile.ObserverStateGame.SubscribeObserver(OnChangeValue);
    }

    private void OnChangeValue(StateGame state)
    {
        switch (state)
        {
            case StateGame.Game:
                
                _backgroundController = new BackgroundController();
                new GenerateLevelController(_generateLevel, new SubscriptionObserver<bool>(), _backgroundController).Init();
                _rightMove.SubscribeObserver(_backgroundController.ChangeSpeed);
                _rightMove.SubscribeObserver(_carController.Move);
                _input.Init(_leftMove, _rightMove, _profile.Car);
                

                break;
            case StateGame.Menu:
                _backgroundController?.Dispose();
                _backgroundController = null;
                _input.Dispose();
                break;
        }
    }
}
