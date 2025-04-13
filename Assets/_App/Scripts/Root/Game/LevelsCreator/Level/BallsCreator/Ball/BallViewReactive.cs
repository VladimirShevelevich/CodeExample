using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;


namespace _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator.Ball
{
    public class BallViewReactive : BaseDisposable
    {
        public readonly ReactiveTrigger HideTrigger = new();
        public readonly ReactiveTrigger OnClicked = new();
        
        public BallViewReactive()
        {
            AddDisposable(HideTrigger);
            AddDisposable(OnClicked);
        }
    }
}