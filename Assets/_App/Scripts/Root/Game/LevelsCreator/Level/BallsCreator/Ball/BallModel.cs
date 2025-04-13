using System;
using _App.Scripts.Content;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;
using UniRx;
using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator.Ball
{
    public class BallModel : BaseDisposable
    {
        public struct Ctx
        {
            public BallViewReactive BallViewReactive;
            public BallInfo BallInfo;
        }

        private readonly Ctx _ctx;

        public BallModel(Ctx ctx)
        {
            _ctx = ctx;
            AddDisposable(_ctx.BallViewReactive.OnClicked.Subscribe(OnClicked));
            SetHiding();
        }

        private void SetHiding()
        {
            AddDisposable(Observable.Timer(TimeSpan.FromSeconds(_ctx.BallInfo.LifeTime)).Subscribe(_ =>
            {
                _ctx.BallViewReactive.HideTrigger.Notify();
            }));
        }

        private void OnClicked()
        {
            Debug.Log("Clicked");                
        }
    }
}