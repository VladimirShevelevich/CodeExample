using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.GemsCreator.Gem
{
    public class GemViewReactive : BaseDisposable
    {
        public readonly ReactiveTrigger OnTriggeredByBall = new ();
        public readonly ReactiveTrigger HideTrigger = new ();
        public readonly ReactiveTrigger OnHidden = new ();

        public GemViewReactive()
        {
            AddDisposable(OnTriggeredByBall);
            AddDisposable(HideTrigger);
        }
    }
}