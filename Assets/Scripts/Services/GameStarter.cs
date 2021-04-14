using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Profile;
using UnityEngine;


public class GameStarter : MonoBehaviour
{
    [SerializeField] private Transform _placeUI;
    [SerializeField] private float _speedCar;
    private BackgroundController _backgroundController;
    void Start()
    {
        var updater = new GameObject("Updater").AddComponent<GameUpdater>();
        var profile = new PlayerProfile(_speedCar);
        var carController = new CarController(profile.Car);
        profile.Observer.SubscribeObserver(carController.ChangeState);
        var menuController = new MainMenuController(_placeUI, profile);
        profile.Observer.SubscribeObserver(OnChangeValue);
    }

    private void OnChangeValue(StateGame state)
    {
        switch (state)
        {
            case StateGame.Game:
                _backgroundController = new BackgroundController();
                break;
            case StateGame.Menu:
                _backgroundController?.Dispose();
                _backgroundController = null;
                break;
        }
    }
}
