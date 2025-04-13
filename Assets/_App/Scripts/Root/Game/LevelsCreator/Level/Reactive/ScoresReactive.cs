using _App.Scripts.Tools.Core;
using UniRx;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.Reactive
{
    public class ScoresReactive : BaseDisposable
    {
        public readonly ReactiveProperty<int> CurrentScore = new();
        
        public ScoresReactive()
        {
            AddDisposable(CurrentScore);
        }
    }
}