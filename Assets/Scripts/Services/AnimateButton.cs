using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Assets.Scripts.Services
{
    [RequireComponent(typeof(AnimateButtonRequire))]
    public class AnimateButton : Button
    {
        private AnimateButtonRequire _require;
        private new void Start()
        {
            base.Start();
            _require = GetComponent<AnimateButtonRequire>();
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            _require.StartTween(AnimateButtonRequire.AnimatePoint.PointEnter, transform, () => {});
            base.OnPointerEnter(eventData);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            _require.StartTween(AnimateButtonRequire.AnimatePoint.PointDown, transform, () => {});
            base.OnPointerDown(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            _require.StartTween(AnimateButtonRequire.AnimatePoint.PointUp, transform, () => {});
            base.OnPointerUp(eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            _require.StartTween(AnimateButtonRequire.AnimatePoint.PointExit, transform, () => {});
            base.OnPointerExit(eventData);
        }
    }
}