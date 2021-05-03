using System;
using UnityEngine;

namespace Assets.Scripts.Features.Battle
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class FinishTrigger : MonoBehaviour
    {
        private Action<bool> _onFinished = delegate(bool b) {  };
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (LayerMask.LayerToName(other.gameObject.layer) == "Enemy")
            {
                _onFinished.Invoke(false);
            }
            else _onFinished.Invoke(false);
        }

        public void Subscription(Action<bool> action)
        {
            _onFinished += action;
        }

        public void Unsubscription(Action<bool> action)
        {
            _onFinished -= action;
        }
    }
}