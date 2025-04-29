using System;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;
using UniRx;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.Reactive
{
    public class LevelTimeReactive : BaseDisposable
    {
        public readonly ReactiveProperty<TimeSpan> TimeLeft = new();
        public readonly ReactiveTrigger OnTimeIsOver = new();
        
        public LevelTimeReactive()
        {
            AddDisposable(TimeLeft);
            AddDisposable(OnTimeIsOver);
        }
    }
}