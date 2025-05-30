using _App.Scripts.Content;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Tools.Core;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.ScoreController
{
    public class ScoreControllerEntity : BaseEntity
    {
        public struct Ctx
        {
            public ScoresReactive ScoresReactive;
            public int ScoreGoal;
        }        
        
        private readonly Ctx _ctx; 
        
        public ScoreControllerEntity(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
        }

        private void AddScore(int score)
        {
            _ctx.ScoresReactive.CurrentScore.Value += score;
            if (_ctx.ScoresReactive.CurrentScore.Value >= _ctx.ScoreGoal)
                _ctx.ScoresReactive.OnScoreGoalCompleted.Notify();
        }
    }
}