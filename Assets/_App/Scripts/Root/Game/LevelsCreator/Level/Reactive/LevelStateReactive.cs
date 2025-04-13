using _App.Scripts.Tools.Core;
using UniRx;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.Reactive
{
    public class LevelStateReactive : BaseDisposable
    {
        public readonly ReactiveProperty<LevelEntity.LevelState> CurrentState = new();
        
        public LevelStateReactive()
        {
            AddDisposable(CurrentState);
        }
    }
}