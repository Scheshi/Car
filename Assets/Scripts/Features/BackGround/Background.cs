using UnityEngine;

namespace Assets.Scripts.BackGround
{
    public class Background : MonoBehaviour, IBackground
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rightBorder;
        [SerializeField] private float _leftBorder;
        
        public void Move(float value)
        {
            transform.position -= Vector3.right * value * _speed;
            var position = transform.position;
            if (position.x <= _leftBorder)
            {
                transform.position = new Vector3(_rightBorder, position.y, position.z);
            }
            if (position.x >= _rightBorder)
            {
                transform.position = new Vector3(_leftBorder, position.y, position.z);
            }
        }

        public static Background CreateBackground(GameObject gameObject, float speed, float rightBorder, float leftBorder)
        {
            var bg = gameObject.AddComponent<Background>();
            bg._speed = speed;
            bg._leftBorder = leftBorder;
            bg._rightBorder = rightBorder;
            return bg;
        }
    }
}