﻿using System;
using _App.Scripts.Root.Game.LevelsCreator.Level;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
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
        private readonly LevelStateReactive _levelStateReactive = new();
        private IDisposable _currentLevel;

        protected override void Initialize()
        {
            AddDisposable(_loadLevelByIndex);
            AddDisposable(_loadLevelByIndex.SubscribeWithSkip(LoadLevelByIndex));
            AddDisposable(_levelStateReactive);
            
            Container.Register(_levelStateReactive);
            CreateModel();
        }

        private void CreateModel()
        {
            AddDisposable(new LevelCreatorModel(new LevelCreatorModel.Ctx
            {
                LoadLevelByIndex = _loadLevelByIndex,
                LevelStateReactive = _levelStateReactive,
                LevelsAmount = Context.LevelsConfigs.Length
            }));
        }

        private void LoadLevelByIndex(int index)
        {
            _currentLevel?.Dispose();
            var levelConfig = Context.LevelsConfigs[index];
            _currentLevel = CreateEntity<LevelEntity, LevelEntity.Ctx>(new LevelEntity.Ctx
            {
                LevelConfig = levelConfig,
                Canvas = Context.Canvas,
                LevelIndex = index
            });
        }
    }
}