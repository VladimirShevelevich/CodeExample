using _App.Scripts.Content;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.Reactive
{
    public class BallsCaughtReactive : BaseDisposable
    {
        public readonly ReactiveEvent<BallInfo> OnCaught = new();
        
        public BallsCaughtReactive()
        {
            AddDisposable(OnCaught);
        }
    }
}