using System.Linq;
using Assets.Scripts.Controllers;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Assets.Scripts.BackGround
{
    public class BackgroundController : BaseController
    {
        private string _pathToPrefabs = "Prefabs/Backgrounds";
        private IBackground[] _backgrounds;
        private float _value;
        private float _speed;

        public BackgroundController(float speed)
        {
            _speed = speed;
        }
        
        private void Execute()
        {
            for (int i = 0; i < _backgrounds.Length; i++)
            {
                _backgrounds[i].Move(_value);
            }
        }

        public void Init()
        {
            LoadBackgrounds();
            GameUpdater.Instance.Add(Execute);
        }

        public override void Dispose()
        {
            GameUpdater.Instance.Remove(Execute);
            base.Dispose();
        }

        private void LoadBackgrounds()
        {
            var background = Resources.LoadAll<Background>(_pathToPrefabs);
            _backgrounds = new IBackground[background.Length];
            for (int i = 0; i < _backgrounds.Length; i++)
            {
                var bg = Object.Instantiate(background[i]);

                AddGameObject(bg.gameObject);
                _backgrounds[i] = bg;
            }
        }

        public void ChangeSpeed(float value)
        {
            _value = value;
        }

        public bool AddBackGround(IBackground background)
        {
            var bg = _backgrounds.FirstOrDefault(x => x == background);
            if (bg != null)
            {
                return false;
            }

            var oldBgs = _backgrounds;
            _backgrounds = new IBackground[oldBgs.Length + 1];
            for (int i = 0; i < oldBgs.Length; i++)
            {
                _backgrounds[i] = oldBgs[i];
            }

            _backgrounds[oldBgs.Length] = background;
            return true;
        }
    }
}