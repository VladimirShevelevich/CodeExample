using System;
using _App.Scripts.Content;
using _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator.Ball;
using _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator.Data;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;
using UniRx;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator
{
    public class BallsCreatorEntity : BaseEntity<BallsCreatorEntity.Ctx>
    {
        public struct Ctx
        {
            public BallsSpawnContent BallsSpawnContent;
        }

        private readonly ReactiveEvent<CreateBallData> _createBall = new();
        
        protected override void Initialize()
        {
            AddDisposable(_createBall);
            AddDisposable(_createBall.SubscribeWithSkip(CreateBall));
            
            CreateModel();
        }

        private void CreateModel()
        {
            AddDisposable(new BallsCreatorModel(new BallsCreatorModel.Ctx
            {
                CreateBall = _createBall,
                BallsSpawnContent = Context.BallsSpawnContent,
            }));
        }

        private void CreateBall(CreateBallData createBallData)
        {
            var entity = CreateEntity<BallEntity, BallEntity.Ctx>(new BallEntity.Ctx
            {
                CreateBallData = createBallData,
                BallsCaughtReactive = Container.Resolve<BallsCaughtReactive>()
            });
            var lifeTime = createBallData.BallInfo.LifeTime;
            AddDisposable(Observable.Timer(TimeSpan.FromSeconds(lifeTime+3)).Subscribe(_ =>
            {
                entity.Dispose();
            }));
        }
    }
}