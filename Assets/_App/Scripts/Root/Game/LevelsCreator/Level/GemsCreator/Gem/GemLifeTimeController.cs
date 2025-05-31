using System;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.GemsCreator.Gem
{
    public class GemLifeTimeController : BaseEntity
    {
        public struct Ctx
        {
            public ReactiveEvent<IDisposable> OnGemHidden;
            public GemViewReactive GemViewReactive;
        }

        private readonly Ctx _ctx;
        private bool _wasHidingTriggered; 
        
        public GemLifeTimeController(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
            AddDisposable(_ctx.GemViewReactive.OnTriggeredByBall.Subscribe(
                OnTriggeredByBall));
            AddDisposable(_ctx.GemViewReactive.OnHidden.Subscribe(() =>
                _ctx.OnGemHidden.Notify(this)));
        }

        private void OnTriggeredByBall()
        {
            if (_wasHidingTriggered)
                return;

            _wasHidingTriggered = true;
            _ctx.GemViewReactive.HideTrigger.Notify();
        }
    }
}