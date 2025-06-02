using System;
using _App.Scripts.Content;
using _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator.Ball;
using _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator.Data;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Tools.Core;
using Random = UnityEngine.Random;
using UniRx;
using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator
{
    public class BallsCreatorEntity : BaseEntity
    {
        public struct Ctx
        {
            public BallsSpawnContent BallsSpawnContent;
            public LevelStateReactive LevelStateReactive;
        }

        private readonly Ctx _ctx; 
        
        public BallsCreatorEntity(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
            AddDisposable(_ctx.LevelStateReactive.CurrentState.Where(state => state == LevelEntity.LevelState.Play)
                .Take(1)
                .Subscribe(_ =>
                {
                    OnPlayStart();
                }));
        }

        private void OnPlayStart()
        {
            AddDisposable(Observable.Timer(TimeSpan.FromSeconds(_ctx.BallsSpawnContent.SpawnInterval))
                .Repeat()
                .Where(_ => _ctx.LevelStateReactive.CurrentState.Value == LevelEntity.LevelState.Play)
                .Subscribe(_ =>
                {
                    CreateNewBall();
                }));
        }

        private void CreateNewBall()
        {
            var randomValue = Random.value;
            var newBallType = randomValue > _ctx.BallsSpawnContent.SpecialBallChance ? BallType.Regular : BallType.Special;
            var ballInfo = _ctx.BallsSpawnContent.GetBallInfoByType(newBallType);
            var spawnArea = _ctx.BallsSpawnContent.SpawnArea;
            var position = new Vector2(Random.Range(-spawnArea.WightRange/2, spawnArea.WightRange/2), spawnArea.Height);
            
            CreateBall(new CreateBallData
            {
                BallInfo = ballInfo,
                Position = position
            });
        }

        private void CreateBall(CreateBallData createBallData)
        {            
            var entity = new BallEntity(new BallEntity.Ctx
                {
                    CreateBallData = createBallData
                },
                Container);
            AddDisposable(entity);
            
            var lifeTime = createBallData.BallInfo.LifeTime;
            AddDisposable(Observable.Timer(TimeSpan.FromSeconds(lifeTime+3)).Subscribe(_ =>
            {
                entity.Dispose();
            }));
        }
    }
}