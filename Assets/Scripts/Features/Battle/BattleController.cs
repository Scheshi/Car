using System.Collections.Generic;
using Assets.Scripts.Controllers;
using Assets.Scripts.Enums;
using Assets.Scripts.Profile;
using UnityEngine;
using Background = Assets.Scripts.BackGround.Background;

namespace Assets.Scripts.Features.Battle
{
    public struct Enemy
    {
        public Enemy(Sprite previewSprite, float speed)
        {
            PreviewSprite = previewSprite;
            Speed = speed;
        }
        public Sprite PreviewSprite;
        public float Speed;
    }
    
    public class BattleController : BaseController
    {
        private string _pathToPrefab = "Prefabs/Battle";
        private string _pathToFinishPrefab = "Prefabs/Finish";
        private BattleView _view;
        private PlayerProfile _profile;

        private List<Enemy> _enemies;
        private CarController _currentEnemy;
        private FinishTrigger _finish;
        private float _currentPlayerSpeed;
        
        
        public BattleController(PlayerProfile profile)
        {
            _enemies = new List<Enemy>()
            {
                new Enemy(Resources.Load<Sprite>("Car/TruckChassisSprite"), 0.03f),
                new Enemy(Resources.Load<Sprite>("Car/TruckChassisSprite"), 0.06f),
                new Enemy(Resources.Load<Sprite>("Car/TruckChassisSprite"), 0.09f),
                new Enemy(Resources.Load<Sprite>("Car/TruckChassisSprite"), 0.13f),
                new Enemy(Resources.Load<Sprite>("Car/TruckChassisSprite"), 0.15f)
            };
            _profile = profile;
        }

        public void Init(Transform uiPlace)
        {
            _view = LoadView<BattleView>(_pathToPrefab, uiPlace);
            if (_profile.WinCount.Value >= _enemies.Count)
            {
                Debug.Log("Вы победили всех врагов!");
                _profile.ObserverStateGame.Value = StateGame.Menu;
            }
            else _view.Init(StartBattle, BackToMenu, _enemies[_profile.WinCount.Value].PreviewSprite, _enemies[_profile.WinCount.Value].Speed);
        }

        private void StartBattle()
        {
            _profile.ObserverStateGame.Value = StateGame.Game;
            var carModel = new Car(_enemies[_profile.WinCount.Value].Speed);
            _currentEnemy = new CarController(carModel);
            _currentEnemy.ChangeState(StateGame.Game);
            _currentEnemy.CarObject.gameObject.layer = LayerMask.NameToLayer("Enemy");
            var childs = _currentEnemy.CarObject.GetComponentsInChildren<Transform>();
            for (int i = 0; i < childs.Length; i++)
            {
                childs[i].gameObject.layer = LayerMask.NameToLayer("Enemy");
            }
            AddController(_currentEnemy);
            _view.gameObject.SetActive(false);
            _finish = Object.Instantiate(Resources.Load<FinishTrigger>(_pathToFinishPrefab), new Vector3(_currentEnemy.CarObject.transform.position.x + 100, _currentEnemy.CarObject.transform.position.y, 0), Quaternion.identity, Object.FindObjectOfType<Background>().transform);
            _finish.gameObject.layer = LayerMask.NameToLayer("Finish");
            AddGameObject(_finish.gameObject);
            _finish.Subscription(OnFinished);
            _profile.Car.CurrentSpeedObserver.SubscribeObserver(ChangePlayerSpeed);
            GameUpdater.Instance.Add(MoveEnemy);
        }

        public override void Dispose()
        {
            _finish?.Unsubscription(OnFinished);
            _currentEnemy?.ChangeState(StateGame.Menu);
            _profile.Car.CurrentSpeedObserver.UnSubscribeObserver(ChangePlayerSpeed);
            GameUpdater.Instance.Remove(MoveEnemy);
            base.Dispose();
        }

        private void OnFinished(bool isPlayer)
        {
            if (isPlayer)
            {
                _profile.WinCount.Value++;
                Debug.Log("Вы выйграли!");
            }
            else Debug.Log("Вы проиграли!");
            BackToMenu();
        }

        private void MoveEnemy()
        {
            _currentEnemy.ChangeSpeedCar(_enemies[_profile.WinCount.Value].Speed - _currentPlayerSpeed);
            _currentEnemy.Move(1);
        }

        private void ChangePlayerSpeed(float value)
        {
            _currentPlayerSpeed = value;
        }

        private void BackToMenu()
        {
            _profile.ObserverStateGame.Value = StateGame.Menu;
        }
    }
}