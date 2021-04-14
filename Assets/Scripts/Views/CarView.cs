using UnityEngine;

namespace Assets.Scripts
{
    public class CarView : MonoBehaviour
    {
        [SerializeField] private HingeJoint2D[] _wheels;

        public void ChangeSpeed(float speed)
        {
            foreach (var wheel in _wheels)
            {
                //wheel.
            }
        }
    }
}