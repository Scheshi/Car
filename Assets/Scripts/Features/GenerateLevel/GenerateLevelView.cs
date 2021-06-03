using System;
using System.Collections;
using Assets.Scripts.Features.GenerateLevel;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Scripts
{
    public class GenerateLevelView : MonoBehaviour, IGenerateLevelView
    {
        public event Action OnComplete = () => { };
         
        [SerializeField] private int _heightPerUnit;
        [SerializeField] private int _startPositionHeight;
        [SerializeField] private int _widthPerUnit = 40;
        [SerializeField] private AssetReferenceGameObject _groundPrefabReference;
        [SerializeField] private AssetReferenceGameObject _dirtPrefabReference;
        [SerializeField] private AssetReferenceGameObject _trianglePrefabReference;
        private GameObject _groundPrefab;
        private GameObject _dirtPrefab;
        private GameObject _trianglePrefab;
        [SerializeField, Range(0, 100)] private int _percentFill;

        private void OnEnable()
        {
            StartCoroutine(LoadPrefabs());
        }

        private IEnumerator LoadPrefabs()
        {
            var groundAsyncOperation = Addressables.LoadAssetAsync<GameObject>(_groundPrefabReference);
            var dirtAsyncOperation = Addressables.LoadAssetAsync<GameObject>(_dirtPrefabReference);
            var triangleAsyncOperation = Addressables.LoadAssetAsync<GameObject>(_trianglePrefabReference);
            yield return groundAsyncOperation;
            yield return dirtAsyncOperation;
            yield return triangleAsyncOperation;
            _trianglePrefab = triangleAsyncOperation.Result;
            _groundPrefab = triangleAsyncOperation.Result;
            _dirtPrefab = dirtAsyncOperation.Result;
            OnComplete.Invoke();
        }

        public int Height => _heightPerUnit;

        public int StartPositionHeight => _startPositionHeight;
        public int Width => _widthPerUnit;
        public GameObject GroundPrefab => _groundPrefab;

        public int PercentFill => _percentFill;

        public GameObject DirtPrefab => _dirtPrefab;

        public GameObject TrianglePrefab => _trianglePrefab;
    }
}