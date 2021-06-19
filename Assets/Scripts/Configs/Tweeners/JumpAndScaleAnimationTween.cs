using System;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Configs.Tweeners
{
    [CreateAssetMenu(menuName =  "Configs/Tweeners/Jump And Scale Animation Tween")]
    public class JumpAndScaleAnimationTween : AnimationTween
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
            _sequence.Append(transform.DOScale(1, Duration));
            _sequence.Append(
                transform
                    .DOMove(transform.position, Duration)
                    .SetLoops(CountLoops)
                    .SetEase(Ease));
            _sequence.Play().OnComplete(onComplete.Invoke);
        }
    }
}