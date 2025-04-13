using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.Reactive
{
    public class LevelUiTriggersReactive : BaseDisposable
    {
        public readonly ReactiveTrigger PlayTrigger = new();
        public readonly ReactiveTrigger NextLevelTrigger = new();
        public readonly ReactiveTrigger RestartTrigger = new();
        
        public LevelUiTriggersReactive()
        {
            AddDisposable(PlayTrigger);
            AddDisposable(NextLevelTrigger);
            AddDisposable(RestartTrigger);
        }
    }
}