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
            public LevelStateReactive LevelStateReactive;
            public LevelTimeReactive LevelTimeReactive;
            public LevelConfig LevelConfig;
        }

        protected override void Initialize()
        {
            Context.LevelTimeReactive.TimeLeft.Value = TimeSpan.FromSeconds(Context.LevelConfig.TimeInSeconds);
            AddDisposable(Context.LevelStateReactive.CurrentState.Where(state => state == LevelEntity.LevelState.Play)
                .Take(1)
                .Subscribe(_ =>
                {
                    ExecuteTimer();
                }));
        }

        private void ExecuteTimer()
        {
            AddDisposable(Observable.Timer(TimeSpan.FromSeconds(1))
                .Repeat()
                .Where(_ => Context.LevelStateReactive.CurrentState.Value == LevelEntity.LevelState.Play)
                .Subscribe(_ =>
                {
                    UpdateTime(1);
                }));
        }

        private void UpdateTime(int secondsDecrease)
        {
            if (Context.LevelTimeReactive.TimeLeft.Value < TimeSpan.FromSeconds(1))
            {
                Context.LevelTimeReactive.OnTimeIsOver.Notify();
                return;
            }
                
            Context.LevelTimeReactive.TimeLeft.Value -= TimeSpan.FromSeconds(secondsDecrease);
        }
    }
}