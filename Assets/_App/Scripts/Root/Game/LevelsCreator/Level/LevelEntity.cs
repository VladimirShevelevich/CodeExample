using _App.Scripts.Content;
using _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator;
using _App.Scripts.Root.Game.LevelsCreator.Level.LevelTimer;
using _App.Scripts.Root.Game.LevelsCreator.Level.LevelUI;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Root.Game.LevelsCreator.Level.ScoreController;
using _App.Scripts.Root.Game.LevelsCreator.Reactive;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Disposables;
using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator.Level
{
    public class LevelEntity : BaseEntity
    {
        public enum LevelState
        {
            Start,
            Play,
            Fail,
            Win
        }
        
        public struct Ctx
        {
            public LevelConfig LevelConfig;
            public Transform Canvas;
            public int LevelIndex;
        }

        private readonly Ctx _ctx; 
        
        private readonly ScoresReactive _scoresReactive = new();
        private readonly LevelTimeReactive _levelTimeReactive = new();
        private readonly LevelStateReactive _levelStateReactive = new();

        public LevelEntity(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
            
            MarkDisposables();
            RegisterDependencies();

            CreateBallsCreator();
            CreateScoresController();
            CreateLevelUI();
            CreateTimer();
            CreateEnvironment();
            CreateLevelStateHandler();
        }

        private void CreateLevelStateHandler()
        {
            var ctx = new LevelStateHandler.Ctx
            {
                LevelStateReactive = _levelStateReactive,
                ScoresReactive = _scoresReactive,
                LevelTimeReactive = _levelTimeReactive
            };
            AddDisposable(new LevelStateHandler(ctx, Container));
        }
        
        private void CreateScoresController()
        {            
            AddDisposable(new ScoreControllerEntity(new ScoreControllerEntity.Ctx
                {
                    ScoresReactive = _scoresReactive,
                    ScoreGoal = _ctx.LevelConfig.ScoreGoal
                },
                Container));
        }

        private void CreateBallsCreator()
        {            
            AddDisposable(new BallsCreatorEntity(new BallsCreatorEntity.Ctx
                {
                    BallsSpawnContent = Container.Resolve<ContentProvider>().BallsSpawnContent,
                    LevelStateReactive = Container.Resolve<LevelStateReactive>()
                },
                Container));
        }

        private void CreateLevelUI()
        {            
            AddDisposable(new LevelUiEntity(new LevelUiEntity.Ctx
                {
                    ScoresReactive = _scoresReactive,
                    LevelTimeReactive = _levelTimeReactive,
                    LevelLoadReactive = Container.Resolve<LevelLoadReactive>(),
                    UiContent = Container.Resolve<ContentProvider>().UiContent,
                    Canvas = _ctx.Canvas,
                    ScoreGoal = _ctx.LevelConfig.ScoreGoal,
                    LevelStateReactive = Container.Resolve<LevelStateReactive>(),
                    LevelIndex = _ctx.LevelIndex
                },
                Container));
        }

        private void CreateTimer()
        {
            AddDisposable(new LevelTimerEntity(new LevelTimerEntity.Ctx
                {
                    LevelTimeReactive = _levelTimeReactive,
                    LevelConfig = _ctx.LevelConfig,
                    LevelStateReactive = Container.Resolve<LevelStateReactive>()
                },
                Container));
        }

        private void CreateEnvironment()
        {
            var view = Object.Instantiate(_ctx.LevelConfig.EnvironmentPrefab);
            AddDisposable(new GameObjectDisposer(view.gameObject));
        }

        private void RegisterDependencies()
        {
            Container.Register<ScoresReactive>(_scoresReactive);
            Container.Register<LevelStateReactive>(_levelStateReactive);
        }

        private void MarkDisposables()
        {
            AddDisposable(_scoresReactive);
            AddDisposable(_levelTimeReactive);
            AddDisposable(_levelStateReactive);
        }
    }
}