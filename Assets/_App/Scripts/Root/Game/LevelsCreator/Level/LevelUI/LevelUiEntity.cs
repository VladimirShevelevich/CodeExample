using _App.Scripts.Content;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Disposables;
using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.LevelUI
{
    public class LevelUiEntity : BaseEntity<LevelUiEntity.Ctx>
    {
        public struct Ctx
        {
            public ScoresReactive ScoresReactive;
            public LevelTimeReactive LevelTimeReactive;
            public LevelStateReactive LevelStateReactive;

            public UiContent UiContent;
            public Transform Canvas;
            public int ScoreGoal;
        }

        protected override void Initialize()
        {
            CreateView();
        }
        
        private void CreateView()
        {
            var view = Object.Instantiate(Context.UiContent.LevelUiPrefab, Context.Canvas);
            view.SetCtx(new LevelUiView.Ctx
            {
                ScoresReactive = Context.ScoresReactive,
                LevelTimeReactive = Context.LevelTimeReactive,
                ScoreGoal = Context.ScoreGoal,
                LevelStateReactive = Context.LevelStateReactive
            });
            AddDisposable(new GameObjectDisposer(view.gameObject));
        }
    }
}