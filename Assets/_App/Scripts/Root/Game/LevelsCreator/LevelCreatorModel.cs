﻿using _App.Scripts.Root.Game.LevelsCreator.Reactive;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;

namespace _App.Scripts.Root.Game.LevelsCreator
{
    public class LevelCreatorModel : BaseDisposable
    {
        public struct Ctx
        {
            public ReactiveEvent<int> LoadLevelByIndex;
            public LevelLoadReactive LevelLoadReactive;
            public int LevelsAmount;
        }

        private readonly Ctx _ctx;
        private int _currentLevelIndex;

        public LevelCreatorModel(Ctx ctx)
        {
            _ctx = ctx;
            _ctx.LoadLevelByIndex.Notify(0);
            
            AddDisposable(_ctx.LevelLoadReactive.RestartTrigger.Subscribe(RestartLevel));
            AddDisposable(_ctx.LevelLoadReactive.NextLevelTrigger.Subscribe(LoadNextLevel));
        }

        private void RestartLevel()
        {
            _ctx.LoadLevelByIndex.Notify(_currentLevelIndex);
        }

        private void LoadNextLevel()
        {
            if (_currentLevelIndex + 1 >= _ctx.LevelsAmount)
                _currentLevelIndex = 0;
            else
                _currentLevelIndex++;
            
            _ctx.LoadLevelByIndex.Notify(_currentLevelIndex);
        }
    }
}