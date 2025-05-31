using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;
using UniRx;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.Reactive
{
    public class ScoresReactive : BaseDisposable
    {
        public readonly ReactiveProperty<int> CurrentScore = new();
        public readonly ReactiveTrigger OnScoreGoalCompleted = new();
        public readonly ReactiveEvent<int> AddScoreTrigger = new();
        
        public ScoresReactive()
        {
            AddDisposable(CurrentScore);
            AddDisposable(OnScoreGoalCompleted);
            AddDisposable(AddScoreTrigger);
        }
    }
}