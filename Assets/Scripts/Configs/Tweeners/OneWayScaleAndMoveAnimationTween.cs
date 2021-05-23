using System;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Configs.Tweeners
{
    [CreateAssetMenu(menuName =  "Configs/Tweeners/One Way Scale And Move Animation Tween")]
    public class OneWayScaleAndMoveAnimationTween : AnimationTween
    {
        public Ease Ease;
        public float Duration;
        public Vector3 MoveValue;
        public int CountLoops;
        public float ScaleValue;
        private Sequence _sequence;
        
        public override void DOAnimation(Transform transform, Action onComplete)
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOMove(transform.position + MoveValue, Duration))
                .SetLoops(CountLoops).SetEase(Ease);
            _sequence.Append(transform.DOScale(ScaleValue, Duration));
            _sequence.Play().OnComplete(() =>
            {
                _sequence = null;
                onComplete.Invoke();
            });
        }
    }
}