using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;
using UniRx;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.Reactive
{
    public class LevelStateReactive : BaseDisposable
    {
        public readonly ReactiveProperty<LevelEntity.LevelState> CurrentState = new();        
        public readonly ReactiveTrigger StartPlayTrigger = new();
        
        public LevelStateReactive()
        {
            AddDisposable(CurrentState);
            AddDisposable(StartPlayTrigger);
        }
    }
}