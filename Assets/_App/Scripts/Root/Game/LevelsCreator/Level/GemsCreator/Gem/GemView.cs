using System;
using _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator.Ball;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.GemsCreator.Gem
{
    public class GemView : MonoBehaviour
    {
        public struct Ctx
        {
            public GemViewReactive GemViewReactive;
        }

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            _ctx.GemViewReactive.HideTrigger.Subscribe(Hide).AddTo(this);
            
            SetRotationLoop();
        }

        private void SetRotationLoop()
        {
            transform.DORotate(new Vector3(0, 360, 0), 4f, RotateMode.FastBeyond360).
                SetRelative(true).
                SetEase(Ease.Linear).
                SetLoops(-1, LoopType.Incremental);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BallView ballView))
            {
                _ctx.GemViewReactive.OnTriggeredByBall.Notify();
            }
        }

        private void Hide()
        {
            transform.DOKill();
            var tweenSequence = DOTween.Sequence();
            tweenSequence.SetLink(gameObject);
            tweenSequence.Append(transform.DORotate(new Vector3(0, 360, 0), .6f, RotateMode.FastBeyond360)
                .SetRelative(true).SetEase(Ease.Linear));
            tweenSequence.Append(transform.DOScale(Vector3.zero, .2f));
            tweenSequence.OnComplete(() => _ctx.GemViewReactive.OnHidden.Notify());
        }
    }
}