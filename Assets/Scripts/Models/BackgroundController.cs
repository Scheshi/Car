using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Interfaces
{
    public class BackgroundController : BaseController
    {
        private string _pathToPrefabs = "Prefabs/Backgrounds";
        private Background[] _backgrounds;
        private float _value = 1;

        public BackgroundController()
        {
            LoadBackgrounds();
            GameUpdater.Instance.Add(Execute);
        }

        private void Execute()
        {
            for (int i = 0; i < _backgrounds.Length; i++)
            {
                _backgrounds[i].Move(_value);
            }
        }

        private void LoadBackgrounds()
        {
            var background = Resources.LoadAll<Background>(_pathToPrefabs);
            _backgrounds = new Background[background.Length];
            for (int i = 0; i < _backgrounds.Length; i++)
            {
                _backgrounds[i] = Object.Instantiate(background[i]);
                AddGameObject(_backgrounds[i].gameObject);
            }
        }

        public void ChangeSpeed(float value)
        {
            _value = value;
        }

        public bool AddBackGround(Background background)
        {
            var bg = _backgrounds.FirstOrDefault(x => x == background);
            if (bg != null)
            {
                return false;
            }

            var oldBgs = _backgrounds;
            _backgrounds = new Background[oldBgs.Length + 1];
            for (int i = 0; i < oldBgs.Length; i++)
            {
                _backgrounds[i] = oldBgs[i];
            }

            _backgrounds[oldBgs.Length] = background;
            return true;
        }
    }
}