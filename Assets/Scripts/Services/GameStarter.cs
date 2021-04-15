using Assets.Scripts.Actions;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Profile;
using UnityEngine;


public class GameStarter : MonoBehaviour
{
    [SerializeField] private Transform _placeUI;
    [SerializeField] private float _speedCar;
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
        
        _profile.Observer.SubscribeObserver(_carController.ChangeState);
        var menuController = new MainMenuController(_placeUI, _profile);
        _profile.Observer.SubscribeObserver(OnChangeValue);
    }

    private void OnChangeValue(StateGame state)
    {
        switch (state)
        {
            case StateGame.Game:
                var leftMove = new SubscriptionObserver<float>();
                var rightMove = new SubscriptionObserver<float>();
                _backgroundController = new BackgroundController();
                rightMove.SubscribeObserver(_backgroundController.ChangeSpeed);
                rightMove.SubscribeObserver(_carController.Move);
                _input = new InputController(leftMove, rightMove, _profile.Car);
                break;
            case StateGame.Menu:
                _backgroundController?.Dispose();
                _backgroundController = null;
                break;
        }
    }
}
