using System;
using _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator.Data;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Disposables;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator.Ball
{
    public class BallEntity : BaseEntity
    {
        public struct Ctx
        {
            public LevelStateReactive LevelStateReactive;
            
            public CreateBallData CreateBallData;
        }

        private readonly BallViewReactive _ballViewReactive = new();
        private readonly Ctx _ctx; 
        private bool _wasClicked;
        
        public BallEntity(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
            AddDisposable(_ballViewReactive);
            AddDisposable(_ballViewReactive.OnClicked.Subscribe(OnClicked));
            SetHiding();
            
            CreateView();
        }

        private void SetHiding()
        {
            AddDisposable(Observable.Timer(TimeSpan.FromSeconds(_ctx.CreateBallData.BallInfo.LifeTime)).Subscribe(_ =>
            {
                if (!_wasClicked)
                    _ballViewReactive.HideTrigger.Notify();
            }));
        }

        private void OnClicked()
        {
            if(_wasClicked)
                return;
            
            if (_ctx.LevelStateReactive.CurrentState.Value != LevelEntity.LevelState.Play)
                return;
            
            _ballViewReactive.HideTrigger.Notify();
            _wasClicked = true;
        }

        private void CreateView()
        {
            var prefab = _ctx.CreateBallData.BallInfo.Prefab;
            var position = new Vector3(_ctx.CreateBallData.Position.x, _ctx.CreateBallData.Position.y, 0);
            var view = Object.Instantiate(prefab, position, Quaternion.identity);
            view.SetCtx(new BallView.Ctx
            {
                BallViewReactive = _ballViewReactive
            });
            AddDisposable(new GameObjectDisposer(view.gameObject));
        }
    }
}