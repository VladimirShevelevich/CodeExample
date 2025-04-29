using _App.Scripts.Content;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Tools.Core;
using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.ScoreController
{
    public class ScoreControllerEntity : BaseEntity
    {
        public struct Ctx
        {
            public BallsCaughtReactive BallsCaughtReactive;
            public ScoresReactive ScoresReactive;
            public int ScoreGoal;
        }        
        
        private readonly Ctx _ctx; 
        
        public ScoreControllerEntity(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
            AddDisposable(_ctx.BallsCaughtReactive.OnCaught.SubscribeWithSkip(OnBallCaught));
        }

        private void OnBallCaught(BallInfo ballInfo)
        {
            AddScore(ballInfo.ScoreReward);
        }

        private void AddScore(int score)
        {
            _ctx.ScoresReactive.CurrentScore.Value += score;
            if (_ctx.ScoresReactive.CurrentScore.Value >= _ctx.ScoreGoal)
                _ctx.ScoresReactive.OnScoreGoalCompleted.Notify();
        }
    }
}