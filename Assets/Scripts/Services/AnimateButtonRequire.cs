using System;
using System.Collections.Generic;
using Assets.Scripts.Configs.Tweeners;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class AnimateButtonRequire : MonoBehaviour
    {
        public enum AnimatePoint
        {
            PointUp,
            PointDown,
            PointExit,
            PointEnter
        }
        
        [SerializeField] private AnimationTween _pointDownTween;
        [SerializeField] private AnimationTween _pointUpTween;
        [SerializeField] private AnimationTween _pointExitTween;
        [SerializeField] private AnimationTween _pointEnterTween;
        private Dictionary<AnimatePoint, AnimationTween> _tweens;
        private Sequence _sequence;

        private void Start()
        {
            _tweens = new Dictionary<AnimatePoint, AnimationTween>()
            {
                {AnimatePoint.PointUp, _pointUpTween},
                {AnimatePoint.PointDown, _pointDownTween},
                {AnimatePoint.PointExit, _pointExitTween},
                {AnimatePoint.PointEnter, _pointEnterTween}
            };
        }
        
        public void StartTween(AnimatePoint point, Transform currentTransform, Action onComplete)
        {
            _tweens[point].DOAnimation(currentTransform, onComplete);
            
        }
    }
}