using _App.Scripts.Content;
using _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Root.Game.LevelsCreator.Level.ScoreController;
using _App.Scripts.Tools.Core;

namespace _App.Scripts.Root.Game.LevelsCreator.Level
{
    public class LevelEntity : BaseEntity<LevelEntity.Ctx>
    {
        public struct Ctx
        {
            public LevelConfig LevelConfig;
        }

        private readonly BallsCaughtReactive _ballsCaughtReactive = new();
        private readonly ScoresReactive _scoresReactive = new();
        
        protected override void Initialize()
        {
            MarkDisposables();
            RegisterDependencies();

            CreateBallsCreator();
            CreateScoresController();
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

        private void RegisterDependencies()
        {
            Container.Register(_ballsCaughtReactive);
            Container.Register(_scoresReactive);
        }

        private void MarkDisposables()
        {
            AddDisposable(_ballsCaughtReactive);
            AddDisposable(_scoresReactive);
        }
    }
}