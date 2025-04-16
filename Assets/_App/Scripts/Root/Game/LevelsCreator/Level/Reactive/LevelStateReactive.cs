using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;
using UniRx;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.Reactive
{
    public interface IReadOnlyLevelStateReactive
    {
        public IReadOnlyReactiveProperty<LevelEntity.LevelState> CurrentStateReadOnly { get; }
        public ReactiveTrigger IPlayTrigger { get; }
    }
    
    public class LevelStateReactive : BaseDisposable, IReadOnlyLevelStateReactive
    {
        public readonly ReactiveProperty<LevelEntity.LevelState> CurrentState = new();        
        public readonly ReactiveTrigger PlayTrigger = new();
        
        public LevelStateReactive()
        {
            AddDisposable(CurrentState);
            AddDisposable(PlayTrigger);
        }

        public IReadOnlyReactiveProperty<LevelEntity.LevelState> CurrentStateReadOnly => CurrentState;
        public ReactiveTrigger IPlayTrigger => PlayTrigger;
    }
}