using _App.Scripts.Content;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Root.Game.LevelsCreator.Reactive;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Disposables;
using UnityEngine;

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
        
        public LevelUiEntity(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
            CreateView();
        }
        
        private void CreateView()
        {
            var view = Object.Instantiate(_ctx.UiContent.LevelUiPrefab, _ctx.Canvas);
            view.SetCtx(new LevelUiView.Ctx
            {
                ScoresReactive = _ctx.ScoresReactive,
                LevelTimeReactive = _ctx.LevelTimeReactive,
                LevelLoadReactive = _ctx.LevelLoadReactive,
                ScoreGoal = _ctx.ScoreGoal,
                LevelStateReactive = _ctx.LevelStateReactive,
                LevelIndex = _ctx.LevelIndex
            });
            AddDisposable(new GameObjectDisposer(view.gameObject));
        }
    }
}