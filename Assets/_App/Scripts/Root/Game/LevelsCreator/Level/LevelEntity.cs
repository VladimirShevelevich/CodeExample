using _App.Scripts.Content;
using _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator;
using _App.Scripts.Root.Game.LevelsCreator.Level.LevelTimer;
using _App.Scripts.Root.Game.LevelsCreator.Level.LevelUI;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Root.Game.LevelsCreator.Level.ScoreController;
using _App.Scripts.Tools.Core;
using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator.Level
{
    public class LevelEntity : BaseEntity<LevelEntity.Ctx>
    {
        public struct Ctx
        {
            public LevelConfig LevelConfig;
            public Transform Canvas;
        }

        private readonly BallsCaughtReactive _ballsCaughtReactive = new();
        private readonly ScoresReactive _scoresReactive = new();
        private readonly LevelTimerReactive _levelTimerReactive = new();
        
        protected override void Initialize()
        {
            MarkDisposables();
            RegisterDependencies();

            CreateBallsCreator();
            CreateScoresController();
            CreateLevelUI();
            CreateTimer();
        }

        private void CreateScoresController()
        {
            CreateEntity<ScoreControllerEntity, ScoreControllerEntity.Ctx>(new ScoreControllerEntity.Ctx
            {
                BallsCaughtReactive = _ballsCaughtReactive,
                ScoresReactive = _scoresReactive
            });
        }

        private void CreateBallsCreator()
        {
            CreateEntity<BallsCreatorEntity, BallsCreatorEntity.Ctx>(new BallsCreatorEntity.Ctx
            {
                BallsSpawnContent = Container.Resolve<ContentProvider>().BallsSpawnContent
            });
        }

        private void CreateLevelUI()
        {
            CreateEntity<LevelUiEntity, LevelUiEntity.Ctx>(new LevelUiEntity.Ctx
            {
                ScoresReactive = _scoresReactive,
                LevelTimerReactive = _levelTimerReactive,
                UiContent = Container.Resolve<ContentProvider>().UiContent,
                Canvas = Context.Canvas
            });
        }

        private void CreateTimer()
        {
            CreateEntity<LevelTimerEntity, LevelTimerEntity.Ctx>(new LevelTimerEntity.Ctx
            {
                LevelTimerReactive = _levelTimerReactive,
                LevelConfig = Context.LevelConfig
            });
        }

        private void RegisterDependencies()
        {
            Container.Register(_ballsCaughtReactive);
            Container.Register(_scoresReactive);
        }

        private void MarkDisposables()
        {
            AddDisposable(_ballsCaughtReactive);
            AddDisposable(_scoresReactive);
            AddDisposable(_levelTimerReactive);
        }
    }
}