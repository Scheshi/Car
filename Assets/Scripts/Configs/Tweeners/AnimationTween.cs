using System;
using UnityEngine;

namespace Assets.Scripts.Configs.Tweeners
{
    
    public abstract class AnimationTween : ScriptableObject
    {
        public abstract void DOAnimation(Transform transform, Action onComplete);
    }
}