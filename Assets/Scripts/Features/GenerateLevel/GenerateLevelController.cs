using Assets.Scripts.Actions;
using Assets.Scripts.BackGround;
using Assets.Scripts.Controllers;
using Assets.Scripts.Features.GenerateLevel;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.GenerateLevel
{
    public class GenerateLevelController : BaseController, IGenerateLevelController
    {
        private string _pathToPrefab = "Prefabs/generateLevel";
        private IGenerateLevelView _view;
        private SubscriptionObserver<bool> _completeObserver;
        private BackgroundController _background;
        private int[,] _levelCurrent;
        
        

        public GenerateLevelController(SubscriptionObserver<bool> completeObserver, BackgroundController backgroundController)
        {
            _view = LoadView<GenerateLevelView>(_pathToPrefab);
            _completeObserver = completeObserver;
            _background = backgroundController;
        }

        public void Init()
        {
            _levelCurrent = new int[_view.Width, _view.Height];
            var rand = new Random();
            for (int x = 0; x < _view.Width; x++)
            {
                for (int y = 0; y < _view.Height; y++)
                {
                    if (y == 0 || rand.Next(0, 100) < _view.PercentFill)
                    {
                        _levelCurrent[x, y] = 1;
                    }
                    else _levelCurrent[x, y] = 0;
                }
            }
            RefactorLevel();
        }

        private void RefactorLevel()
        {
            for (int x = 0; x < _view.Width; x++)
            {
                for (int y = 0; y < _view.Height; y++)
                {
                    try
                    {
                        if (_levelCurrent[x, y] == 1)
                        {
                            if ((y - 1 > 0 && _levelCurrent[x, y - 1] == 0) || (x - 1 > 0 && x + 1 < _view.Width && _levelCurrent[x - 1, y] == 0 && _levelCurrent[x + 1, y] == 0)) _levelCurrent[x, y] = 0;
                            else if (x - 1 > 0 && y - 1 > 0 && _levelCurrent[x - 1, y - 1] == 0)
                            {
                                _levelCurrent[x, y] = 0;
                            }
                            else
                            {
                                for (int i = 0; i < y; i++)
                                {
                                    _levelCurrent[x, i] = 2;
                                }

                                if (x - 1 > 0 && y - 1 > 0 && _levelCurrent[x - 1, y - 1] == 1)
                                {
                                    _levelCurrent[x - 1, y - 1] = 3;
                                    _levelCurrent[x, y] = 3;
                                }

                            }
                        }
                    }
                    catch
                    {
                        Debug.LogWarning($"x = {x}, y = {y} - IndexOutOfRangeException");
                    }
                }
            }
            Drawing();
        }

        private void Drawing()
        {
            var ground = new GameObject("Ground");
            for (int x = 0; x < _view.Width; x++)
            {
                for (int y = 0; y < _view.Height; y++)
                {
                    if (_levelCurrent[x, y] == 1)
                    {
                        var go = Object.Instantiate(_view.GroundPrefab, new Vector3(x, _view.StartPositionHeight + y, 0), Quaternion.identity, ground.transform);
                        go.layer = LayerMask.NameToLayer("Ground");
                        go.AddComponent<BoxCollider2D>().size = new Vector2(1.0f, 1.0f);
                    }
                    else if (_levelCurrent[x, y] == 2)
                    {
                        var go = Object.Instantiate(_view.DirtPrefab, new Vector3(x, _view.StartPositionHeight + y, 0), Quaternion.identity, ground.transform);
                        go.layer = LayerMask.NameToLayer("Ground");
                        go.AddComponent<BoxCollider2D>().size = new Vector2(1.0f, 1.0f);
                    }
                    else if (_levelCurrent[x, y] == 3)
                    {
                        var go = Object.Instantiate(_view.TrianglePrefab, new Vector3(x, _view.StartPositionHeight + y, 0), Quaternion.identity, ground.transform);
                        go.layer = LayerMask.NameToLayer("Ground");
                    }
                }
            }
            var bg = Background.CreateBackground(ground, 0.08f, -16.9f, 16.9f - _view.Width);
            AddGameObject(bg.gameObject);
            _completeObserver.Value = _background.AddBackGround(bg);
        }
    }
}