using System;
using _App.Scripts.Content;
using _App.Scripts.Root.Game.LevelsCreator.Level.GemsCreator.Gem;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.GemsCreator
{
    public class GemsCreatorEntity : BaseEntity
    {
        public struct Ctx
        {
            public LevelStateReactive LevelStateReactive;
            public GemsContent GemsContent;
        }

        private readonly Ctx _ctx;
        private readonly ReactiveEvent<IDisposable> _onGemHidden = new();
        
        public GemsCreatorEntity(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
            AddDisposable(_onGemHidden);
            AddDisposable(_onGemHidden.SubscribeWithSkip(OnGemHidden));
            AddDisposable(_ctx.LevelStateReactive.CurrentState.Where(state => state == LevelEntity.LevelState.Play)
                .Take(1)
                .Subscribe(_ =>
                {
                    OnPlayStart();
                }));
        }
        
        private void OnPlayStart()
        {
            CreateGem();
        }
        
        private void CreateGem()
        {
            var spawnArea = _ctx.GemsContent.SpawnArea;
            var position = new Vector2(Random.Range(-spawnArea.WightRange/2, spawnArea.WightRange/2), 
                Random.Range(-spawnArea.HeightRange/2, spawnArea.HeightRange/2));
            var createGemData = new CreateGemData
            {
                Position = position
            };

            var ctx = new GemEntity.Ctx
            {
                OnGemHidden = _onGemHidden,
                GemsContent = _ctx.GemsContent,
                CreateGemData = createGemData
            };
            var entity = new GemEntity(ctx, Container);
            AddDisposable(entity);
        }

        private void OnGemHidden(IDisposable hiddenGemEntity)
        {
            hiddenGemEntity.Dispose();
            CreateGem();
        }
    }
}