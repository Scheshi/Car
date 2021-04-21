using Assets.Scripts.Controllers;
using Assets.Scripts.Profile;
using Assets.Scripts.Services;
using UnityEngine;


public class GameStarter : MonoBehaviour
{
    [SerializeField] private Transform _placeUI;
    [SerializeField] private float _speedCar;
    private MainController _controller;
    private PlayerProfile _profile;
    
    
    void Start()
    {
        new GameObject("Updater").AddComponent<GameUpdater>();
        _profile = new PlayerProfile(_speedCar, new UnityAnalytic());
        _controller = new MainController(_profile, _placeUI);
    }

    
}
