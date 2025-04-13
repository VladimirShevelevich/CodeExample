using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Tools.Core;

namespace _App.Scripts.Root.Game.LevelsCreator.Level
{
    public class LevelModel : BaseDisposable
    {
        public struct Ctx
        {
            public LevelTimeReactive LevelTimeReactive;
            public LevelStateReactive LevelStateReactive;
            public LevelUiTriggersReactive LevelUiTriggersReactive;
        }

        private readonly Ctx _ctx;

        public LevelModel(Ctx ctx)
        {
            _ctx = ctx;
            AddDisposable(_ctx.LevelTimeReactive.OnTimeIsOver);
            AddDisposable(_ctx.LevelUiTriggersReactive.PlayTrigger.Subscribe(OnPlayTrigger));

            _ctx.LevelStateReactive.CurrentState.Value = LevelEntity.LevelState.Start;
        }

        private void OnPlayTrigger()
        {
            _ctx.LevelStateReactive.CurrentState.Value = LevelEntity.LevelState.Play;
        }

        private void OnTimeIsOver()
        {
            
        }
    }
}