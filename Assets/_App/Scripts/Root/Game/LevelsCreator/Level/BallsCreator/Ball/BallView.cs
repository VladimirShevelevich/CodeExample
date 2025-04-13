using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator.Ball
{
    public class BallView : MonoBehaviour
    {
        public struct Ctx
        {
            public BallViewReactive BallViewReactive;
        }

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            _ctx.BallViewReactive.HideTrigger.Subscribe(Hide).AddTo(this);
        }

        private void Hide()
        {
            transform.DOScale(0, 0.1f).
                OnComplete(()=> gameObject.SetActive(false)).
                SetLink(gameObject);
        }

        void OnMouseDown()
        {
            _ctx.BallViewReactive.OnClicked.Notify();
        }
    }
}