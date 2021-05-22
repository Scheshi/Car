using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Configs.Tweeners
{
    [CreateAssetMenu(menuName =  "Configs/Tweeners/AnimationTween")]
    public class AnimationTween : ScriptableObject
    {
        public Ease Ease;
        public float Duration;
        public Vector3 MoveValue;
        public int CountLoops;
        public float ScaleValue;

    }
}