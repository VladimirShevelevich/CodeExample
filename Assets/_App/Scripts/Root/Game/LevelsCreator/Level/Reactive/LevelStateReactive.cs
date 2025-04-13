using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;
using UniRx;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.Reactive
{
    public class LevelStateReactive : BaseDisposable
    {
        public readonly ReactiveProperty<LevelEntity.LevelState> CurrentState = new();        
        public readonly ReactiveTrigger PlayTrigger = new();
        public readonly ReactiveTrigger NextLevelTrigger = new();
        public readonly ReactiveTrigger RestartTrigger = new();
        
        public LevelStateReactive()
        {
            AddDisposable(CurrentState);
            AddDisposable(PlayTrigger);
            AddDisposable(NextLevelTrigger);
            AddDisposable(RestartTrigger);
        }
    }
}