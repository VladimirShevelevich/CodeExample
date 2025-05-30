using System;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Tools.Core;
using UniRx;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.LevelTimer
{
    public class LevelTimerEntity : BaseEntity
    {
        public struct Ctx
        {
            public LevelStateReactive LevelStateReactive;
            public LevelTimeReactive LevelTimeReactive;
            public LevelConfig LevelConfig;
        }
        
        private readonly Ctx _ctx; 
        
        public LevelTimerEntity(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
            
            SetInitialTime();
            AddDisposable(_ctx.LevelStateReactive.CurrentState.Where(state => state == LevelEntity.LevelState.Play)
                .Take(1)
                .Subscribe(_ =>
                {
                    ExecuteTimer();
                }));
        }

        private void SetInitialTime()
        {
            _ctx.LevelTimeReactive.TimeLeft.Value = TimeSpan.FromSeconds(_ctx.LevelConfig.TimeInSeconds);
        }

        private void ExecuteTimer()
        {
            AddDisposable(Observable.Timer(TimeSpan.FromSeconds(1))
                .Repeat()
                .Where(_ => _ctx.LevelStateReactive.CurrentState.Value == LevelEntity.LevelState.Play)
                .Subscribe(_ =>
                {
                    UpdateTime(1);
                }));
        }

        private void UpdateTime(int secondsDecrease)
        {
            if (_ctx.LevelTimeReactive.TimeLeft.Value < TimeSpan.FromSeconds(1))
            {
                _ctx.LevelTimeReactive.OnTimeIsOver.Notify();
                return;
            }
                
            _ctx.LevelTimeReactive.TimeLeft.Value -= TimeSpan.FromSeconds(secondsDecrease);
        }
    }
}