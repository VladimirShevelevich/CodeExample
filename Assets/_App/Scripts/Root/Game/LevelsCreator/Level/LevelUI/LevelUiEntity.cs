using System;
using _App.Scripts.Content;
using _App.Scripts.Root.Game.LevelsCreator.Level.LevelUI.UpgradePopup;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Root.Game.LevelsCreator.Reactive;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Disposables;
using UnityEngine;
using UniRx;
using Object = UnityEngine.Object;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.LevelUI
{
    public class LevelUiEntity : BaseEntity
    {
        public struct Ctx
        {
            public ScoresReactive ScoresReactive;
            public LevelLoadReactive LevelLoadReactive;
            public LevelTimeReactive LevelTimeReactive;
            public LevelStateReactive LevelStateReactive;

            public UiContent UiContent;
            public Transform Canvas;
            public int ScoreGoal;
            public int LevelIndex;
        }        
        
        private readonly Ctx _ctx;
        private readonly LevelUiViewReactive _viewReactive = new();
        
        public LevelUiEntity(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
            AddDisposable(_viewReactive);
            AddDisposable(_ctx.LevelStateReactive.CurrentState.Subscribe(OnLevelStateChanged));
            AddDisposable(_ctx.ScoresReactive.CurrentScore.Subscribe(OnCurrentScoreChanged));
            AddDisposable(_ctx.LevelTimeReactive.TimeLeft.Subscribe(OnTimeLeftChanged));
            
            AddDisposable(_viewReactive.OnPlayButtonClicked.Subscribe(OnPlayClick));
            AddDisposable(_viewReactive.OnRestartClicked.Subscribe(OnRestartClick));
            AddDisposable(_viewReactive.OnNextLevelClicked.Subscribe(OnNextLevelClick));
            
            SetScoreGoal();
            SetLevelIndex();
            CreateUpgradePopupCreator();
            CreateView();
        }

        private void SetScoreGoal()
        {
            _viewReactive.ScoreGoal.Value = _ctx.ScoreGoal;
        }
        
        private void SetLevelIndex()
        {
            _viewReactive.LevelIndex.Value = _ctx.LevelIndex + 1;
        }

        private void OnLevelStateChanged(LevelEntity.LevelState levelState)
        {
            _viewReactive.CurrentState.Value = levelState;
        }

        private void OnCurrentScoreChanged(int currentScore)
        {
            _viewReactive.CurrentScore.Value = currentScore;
        }

        private void OnTimeLeftChanged(TimeSpan timeLeft)
        {
            _viewReactive.TimeLeft.Value = timeLeft;
        }

        private void OnPlayClick()
        {
            _ctx.LevelStateReactive.StartPlayTrigger.Notify();
        }

        private void OnNextLevelClick()
        {
            _ctx.LevelLoadReactive.NextLevelTrigger.Notify();
        }

        private void OnRestartClick()
        {
            _ctx.LevelLoadReactive.RestartTrigger.Notify();
        }

        private void CreateUpgradePopupCreator()
        {
            var ctx = new UpgradePopupCreatorEntity.Ctx
            {

            };
            AddDisposable(new UpgradePopupCreatorEntity(ctx, Container));
        }
        
        private void CreateView()
        {
            var view = Object.Instantiate(_ctx.UiContent.LevelUiPrefab, _ctx.Canvas);
            view.SetCtx(new LevelUiView.Ctx
            {
                ViewReactive = _viewReactive
            });
            AddDisposable(new GameObjectDisposer(view.gameObject));
        }
    }
}