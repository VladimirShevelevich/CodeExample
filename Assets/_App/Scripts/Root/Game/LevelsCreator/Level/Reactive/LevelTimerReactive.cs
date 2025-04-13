using System;
using _App.Scripts.Tools.Core;
using UniRx;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.Reactive
{
    public class LevelTimerReactive : BaseDisposable
    {
        public readonly ReactiveProperty<TimeSpan> TimeLeft = new();
        
        public LevelTimerReactive()
        {
            AddDisposable(TimeLeft);
        }
    }
}