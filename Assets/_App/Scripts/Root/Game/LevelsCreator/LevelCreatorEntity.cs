using System;
using _App.Scripts.Root.Game.LevelsCreator.Level;
using _App.Scripts.Root.Game.LevelsCreator.Reactive;
using _App.Scripts.Tools.Core;
using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator
{
    public class LevelCreatorEntity : BaseEntity
    {
        public struct Ctx
        {
            public LevelConfig[] LevelsConfigs;
            public Transform Canvas;
        }

        private readonly Ctx _ctx; 
        private int _currentLevelIndex;
        
        private readonly LevelLoadReactive _levelLoadReactive = new();
        private IDisposable _currentLevel;

        public LevelCreatorEntity(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
            Container.Register<LevelLoadReactive>(_levelLoadReactive);
            
            AddDisposable(_levelLoadReactive);
            AddDisposable(_levelLoadReactive.RestartTrigger.Subscribe(RestartLevel));
            AddDisposable(_levelLoadReactive.NextLevelTrigger.Subscribe(LoadNextLevel));
            
            LoadLevelByIndex(0);
        }

        private void RestartLevel()
        {
            LoadLevelByIndex(_currentLevelIndex);
        }

        private void LoadLevelByIndex(int index)
        {
            _currentLevel?.Dispose();
            var levelConfig = _ctx.LevelsConfigs[index];
            
            var entity = new LevelEntity(new LevelEntity.Ctx
                {
                    LevelConfig = levelConfig,
                    Canvas = _ctx.Canvas,
                    LevelIndex = index
                },
                Container);
            _currentLevel = entity;
            AddDisposable(entity);
        }

        private void LoadNextLevel()
        {
            if (_currentLevelIndex + 1 >= _ctx.LevelsConfigs.Length)
                _currentLevelIndex = 0;
            else
                _currentLevelIndex++;
            
            LoadLevelByIndex(_currentLevelIndex);
        }
    }
}