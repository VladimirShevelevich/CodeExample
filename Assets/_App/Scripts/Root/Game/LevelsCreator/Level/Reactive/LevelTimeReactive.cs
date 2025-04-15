using System;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;
using UniRx;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.Reactive
{
    public interface IReadOnlyLevelTimeReactive
    {
        public IReadOnlyReactiveProperty<TimeSpan> ITimeLeft { get; }
        public ReactiveTrigger IOnTimeIsOver { get; }
    }
    
    public class LevelTimeReactive : BaseDisposable, IReadOnlyLevelTimeReactive
    {
        public readonly ReactiveProperty<TimeSpan> TimeLeft = new();
        public readonly ReactiveTrigger OnTimeIsOver = new();
        
        public LevelTimeReactive()
        {
            AddDisposable(TimeLeft);
            AddDisposable(OnTimeIsOver);
        }

        public IReadOnlyReactiveProperty<TimeSpan> ITimeLeft => TimeLeft;
        public ReactiveTrigger IOnTimeIsOver => OnTimeIsOver;
    }
}