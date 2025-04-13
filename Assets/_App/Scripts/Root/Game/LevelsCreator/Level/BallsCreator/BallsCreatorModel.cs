using System;
using _App.Scripts.Content;
using _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator.Data;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator
{
    public class BallsCreatorModel : BaseDisposable
    {
        public struct Ctx
        {
            public ReactiveEvent<CreateBallData> CreateBall;
            
            public BallsSpawnContent BallsSpawnContent;
        }

        private readonly Ctx _ctx;

        public BallsCreatorModel(Ctx ctx)
        {
            _ctx = ctx;
            AddDisposable(Observable.Timer(TimeSpan.FromSeconds(_ctx.BallsSpawnContent.SpawnInterval)).Repeat().Subscribe(_ =>
            {
                CreateNewBall();
            }));
            CreateNewBall();
        }

        private void CreateNewBall()
        {
            var randomValue = Random.value;
            var newBallType = randomValue > _ctx.BallsSpawnContent.SpecialBallChance ? BallType.Regular : BallType.Special;
            var ballInfo = _ctx.BallsSpawnContent.GetBallInfoByType(newBallType);
            var spawnArea = _ctx.BallsSpawnContent.SpawnArea;
            var position = new Vector2(Random.Range(-spawnArea.Wight/2, spawnArea.Wight/2), Random.Range(-spawnArea.Height/2, spawnArea.Height/2));
            _ctx.CreateBall.Notify(new CreateBallData
            {
                BallInfo = ballInfo,
                Position = position
            });
        }
    }
}