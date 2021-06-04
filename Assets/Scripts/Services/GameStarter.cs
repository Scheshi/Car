using System;
using Assets.Scripts.Controllers;
using Assets.Scripts.Profile;
using Assets.Scripts.Services;
using NotificationSamples;
using UnityEngine;


public class GameStarter : MonoBehaviour
{
    [SerializeField] private Transform _placeUI;
    [SerializeField] private float _speedCar;
    private MainController _controller;
    private PlayerProfile _profile;
    public static GameNotificationsManager NotificationsManager;
    
    
    private void Start()
    {
        new GameObject("Updater").AddComponent<GameUpdater>();
        NotificationsManager = new GameObject("Notification").AddComponent<GameNotificationsManager>();
        NotificationsManager.Initialize(new GameNotificationChannel("Test", "Test", "Test"));
        _profile = new PlayerProfile(_speedCar, new UnityAnalytic());
        _controller = new MainController(_profile, _placeUI);
    }

    private void OnDestroy()
    {
        Destroy(NotificationsManager.gameObject);
        _controller.Dispose();
    }

    
}
