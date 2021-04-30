using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Features.Battle
{
    public class BattleView : MonoBehaviour
    {
        [SerializeField] private Button _startBattle;
        [SerializeField] private Button _backToMenu;
        [SerializeField] private Text _speedText;
        [SerializeField] private Transform _positionForCreateEnemy;
        private GameObject _enemyObject;

        public void Init(Action startBattle, Action backToMenu, GameObject carEnemy, float speed)
        {
            _startBattle.onClick.AddListener(startBattle.Invoke);
            _backToMenu.onClick.AddListener(backToMenu.Invoke);
            _enemyObject = Instantiate(carEnemy, _positionForCreateEnemy);
            _speedText.text = "Speed: " + speed;
            if (_enemyObject.TryGetComponent<Rigidbody2D>(out var rigidbody2D))
            {
                rigidbody2D.gravityScale = 0.0f;
            }
        }

        private void OnDestroy()
        {
            Destroy(_enemyObject.gameObject);
            _startBattle.onClick.RemoveAllListeners();
            _backToMenu.onClick.RemoveAllListeners();
        }
    }
}