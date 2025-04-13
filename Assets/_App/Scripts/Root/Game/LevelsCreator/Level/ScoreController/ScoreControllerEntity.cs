using _App.Scripts.Content;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Tools.Core;
using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.ScoreController
{
    public class ScoreControllerEntity : BaseEntity<ScoreControllerEntity.Ctx>
    {
        public struct Ctx
        {
            public BallsCaughtReactive BallsCaughtReactive;
            public ScoresReactive ScoresReactive;
        }

        protected override void Initialize()
        {
            AddDisposable(Context.BallsCaughtReactive.OnCaught.SubscribeWithSkip(OnBallCaught));
        }

        private void OnBallCaught(BallInfo ballInfo)
        {
            AddScore(ballInfo.ScoreReward);
        }

        private void AddScore(int score)
        {
            Context.ScoresReactive.CurrentScore.Value += score;
            Debug.Log(Context.ScoresReactive.CurrentScore.Value);
        }
    }
}