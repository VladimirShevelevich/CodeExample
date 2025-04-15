using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;

namespace _App.Scripts.Root.Game.LevelsCreator.Reactive
{
    public class LevelLoadReactive : BaseDisposable
    {
        public readonly ReactiveTrigger NextLevelTrigger = new();
        public readonly ReactiveTrigger RestartTrigger = new();
        
        public LevelLoadReactive()
        {
            AddDisposable(NextLevelTrigger);
            AddDisposable(RestartTrigger);
        }
    }
}