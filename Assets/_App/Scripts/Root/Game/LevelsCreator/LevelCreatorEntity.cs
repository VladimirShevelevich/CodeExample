using System;
using _App.Scripts.Root.Game.LevelsCreator.Level;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;
using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator
{
    public class LevelCreatorEntity : BaseEntity<LevelCreatorEntity.Ctx>
    {
        public struct Ctx
        {
            public LevelConfig[] LevelsConfigs;
            public Transform Canvas;
        }

        private readonly ReactiveEvent<int> _loadLevelByIndex = new();
        private IDisposable _currentLevel;

        protected override void Initialize()
        {
            AddDisposable(_loadLevelByIndex);
            AddDisposable(_loadLevelByIndex.SubscribeWithSkip(LoadLevelByIndex));
            
            CreateModel();
        }

        private void CreateModel()
        {
            AddDisposable(new LevelCreatorModel(new LevelCreatorModel.Ctx
            {
                LoadLevelByIndex = _loadLevelByIndex
            }));
        }

        private void LoadLevelByIndex(int index)
        {
            _currentLevel?.Dispose();
            var levelConfig = Context.LevelsConfigs[index];
            CreateEntity<LevelEntity, LevelEntity.Ctx>(new LevelEntity.Ctx
            {
                LevelConfig = levelConfig,
                Canvas = Context.Canvas
            });
        }
    }
}