using System;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Tools.Core;
using UniRx;
using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.LevelTimer
{
    public class LevelTimerEntity : BaseEntity<LevelTimerEntity.Ctx>
    {
        public struct Ctx
        {
            public LevelTimerReactive LevelTimerReactive;
            public LevelConfig LevelConfig;
        }

        protected override void Initialize()
        {
            ExecuteTimer();
        }

        private void ExecuteTimer()
        {
            Context.LevelTimerReactive.TimeLeft.Value = TimeSpan.FromSeconds(Context.LevelConfig.TimeInSeconds);
            AddDisposable(Observable.Timer(TimeSpan.FromSeconds(1)).Repeat().Subscribe(_ =>
            {
                UpdateTime(1);
            }));
        }

        private void UpdateTime(int secondsDecrease)
        {
            Context.LevelTimerReactive.TimeLeft.Value -= TimeSpan.FromSeconds(secondsDecrease);
        }
    }
}