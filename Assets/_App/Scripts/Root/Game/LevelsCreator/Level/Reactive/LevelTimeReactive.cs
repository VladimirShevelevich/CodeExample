using System;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;
using UniRx;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.Reactive
{
    public interface IReadOnlyLevelTimeReactive
    {
        public IReadOnlyReactiveProperty<TimeSpan> TimeLeftReadOnly { get; }
        public ReactiveTrigger OnTimeIsOverReadOnly { get; }
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

        public IReadOnlyReactiveProperty<TimeSpan> TimeLeftReadOnly => TimeLeft;
        public ReactiveTrigger OnTimeIsOverReadOnly => OnTimeIsOver;
    }
}