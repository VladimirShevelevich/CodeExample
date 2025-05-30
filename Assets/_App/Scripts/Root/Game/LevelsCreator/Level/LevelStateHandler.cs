using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Tools.Core;

namespace _App.Scripts.Root.Game.LevelsCreator.Level
{
    public class LevelStateHandler : BaseEntity
    {
        public struct Ctx
        {
            public LevelStateReactive LevelStateReactive;
            public ScoresReactive ScoresReactive;
            public LevelTimeReactive LevelTimeReactive;
        }
        
        private readonly Ctx _ctx; 
        
        public LevelStateHandler(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
            
            AddDisposable(_ctx.LevelTimeReactive.OnTimeIsOver.Subscribe(OnTimeIsOver));
            AddDisposable(_ctx.ScoresReactive.OnScoreGoalCompleted.Subscribe(OnScoreGoalCompleted));
            AddDisposable(_ctx.LevelStateReactive.StartPlayTrigger.Subscribe(OnStartPlayTrigger));
            
            _ctx.LevelStateReactive.CurrentState.Value = LevelEntity.LevelState.Start;
        }
        
        private void OnStartPlayTrigger()
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