using _App.Scripts.Content;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Tools.Core;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.GemsCreator.Gem
{
    public class GemScoreController : BaseEntity
    {
        public struct Ctx
        {
            public ScoresReactive ScoresReactive;
            public GemsContent GemsContent;
            public GemViewReactive GemViewReactive;
        }

        private readonly Ctx _ctx;

        private bool _wasAddScoreTriggered; 
        
        public GemScoreController(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
            AddDisposable(_ctx.GemViewReactive.OnTriggeredByBall.Subscribe(OnTriggeredByBall));
        }

        private void OnTriggeredByBall()
        {
            if (_wasAddScoreTriggered)
                return;

            _wasAddScoreTriggered = true;
            TriggerScoreAdd();
        }

        private void TriggerScoreAdd()
        {
            var score = _ctx.GemsContent.Reward;
            _ctx.ScoresReactive.AddScoreTrigger.Notify(score);
        }
    }
}