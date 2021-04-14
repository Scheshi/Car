using Assets.Scripts.Interfaces;
using Assets.Scripts.Profile;
using UnityEngine;


public class GameStarter : MonoBehaviour
{
    [SerializeField] private Transform _placeUI;
    [SerializeField] private float _speedCar;
    void Start()
    {
        var updater = new GameObject("Updater").AddComponent<GameUpdater>();
        var profile = new PlayerProfile(_speedCar);
        var carController = new CarController(profile.Car);
        profile.Observer.SubscribeObserver(carController.ChangeState);
        var menuController = new MainMenuController(_placeUI, profile);

    }
}
