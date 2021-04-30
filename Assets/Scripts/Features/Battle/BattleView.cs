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
        [SerializeField] private Image _positionForCreateEnemy;
        private Image _enemyObject;

        public void Init(Action startBattle, Action backToMenu, Sprite carEnemy, float speed)
        {
            _startBattle.onClick.AddListener(startBattle.Invoke);
            _backToMenu.onClick.AddListener(backToMenu.Invoke);
            _positionForCreateEnemy.sprite = carEnemy;
            _speedText.text = "Speed: " + speed;
        }

        private void OnDestroy()
        {
            _startBattle.onClick.RemoveAllListeners();
            _backToMenu.onClick.RemoveAllListeners();
        }
    }
}