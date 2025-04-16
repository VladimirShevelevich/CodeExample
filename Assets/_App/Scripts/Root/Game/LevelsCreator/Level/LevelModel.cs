using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Tools.Core;

namespace _App.Scripts.Root.Game.LevelsCreator.Level
{
    public class LevelModel : BaseDisposable
    {
        public struct Ctx
        {
            public LevelStateReactive LevelStateReactive;
            public IReadOnlyScoresReactive ScoresReactive;
            public IReadOnlyLevelTimeReactive LevelTimeReactive;
        }

        private readonly Ctx _ctx;

        public LevelModel(Ctx ctx)
        {
            _ctx = ctx;
            AddDisposable(_ctx.LevelTimeReactive.OnTimeIsOverReadOnly.Subscribe(OnTimeIsOver));
            AddDisposable(_ctx.ScoresReactive.OnScoreGoalCompletedReadOnly.Subscribe(OnScoreGoalCompleted));
            AddDisposable(_ctx.LevelStateReactive.PlayTrigger.Subscribe(OnPlayTrigger));

            _ctx.LevelStateReactive.CurrentState.Value = LevelEntity.LevelState.Start;
        }

        private void OnPlayTrigger()
        {
            _ctx.LevelStateReactive.CurrentState.Value = LevelEntity.LevelState.Play;
        }

        private void OnScoreGoalCompleted()
        {
            _ctx.LevelStateReactive.CurrentState.Value = LevelEntity.LevelState.Win;
        }

        private void OnTimeIsOver()
        {
            _ctx.LevelStateReactive.CurrentState.Value = LevelEntity.LevelState.Fail;
        }
    }
}