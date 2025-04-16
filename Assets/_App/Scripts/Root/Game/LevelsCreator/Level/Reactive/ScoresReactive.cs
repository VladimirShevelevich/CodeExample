using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;
using UniRx;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.Reactive
{
    public interface IReadOnlyScoresReactive
    {
        public IReadOnlyReactiveProperty<int> CurrentScoreReadOnly { get; }
        public IReadOnlyReactiveTrigger OnScoreGoalCompletedReadOnly { get; }
    }
    
    public class ScoresReactive : BaseDisposable, IReadOnlyScoresReactive
    {
        public readonly ReactiveProperty<int> CurrentScore = new();
        public readonly ReactiveTrigger OnScoreGoalCompleted = new();
        
        public ScoresReactive()
        {
            AddDisposable(CurrentScore);
            AddDisposable(OnScoreGoalCompleted);
        }

        public IReadOnlyReactiveProperty<int> CurrentScoreReadOnly => CurrentScore;
        public IReadOnlyReactiveTrigger OnScoreGoalCompletedReadOnly => OnScoreGoalCompleted;
    }
}