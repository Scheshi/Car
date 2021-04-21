using UnityEngine;

namespace Assets.Scripts
{
    public class GenerateLevelView : MonoBehaviour
    {
        [SerializeField] private int _heightPerUnit;
        [SerializeField] private int _startPositionHeight;
        [SerializeField] private int _widthPerUnit = 40;
        [SerializeField] private GameObject _groundPrefab;
        [SerializeField] private GameObject _dirtPrefab;
        [SerializeField] private GameObject _trianglePrefab;
        [SerializeField, Range(0, 100)] private int _percentFill;

        public int Height => _heightPerUnit;

        public int StartPositionHeight => _startPositionHeight;
        public int Width => _widthPerUnit;
        public GameObject GroundPrefab => _groundPrefab;

        public int PercentFill => _percentFill;

        public GameObject DirtPrefab => _dirtPrefab;

        public GameObject TrianglePrefab => _trianglePrefab;
    }
}