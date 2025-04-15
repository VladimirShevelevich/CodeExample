using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;
using UniRx;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.Reactive
{
    public interface IReadOnlyScoresReactive
    {
        public IReadOnlyReactiveProperty<int> ICurrentScore { get; }
        public IReadOnlyReactiveTrigger IOnScoreGoalCompleted { get; }
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

        public IReadOnlyReactiveProperty<int> ICurrentScore => CurrentScore;
        public IReadOnlyReactiveTrigger IOnScoreGoalCompleted => OnScoreGoalCompleted;
    }
}