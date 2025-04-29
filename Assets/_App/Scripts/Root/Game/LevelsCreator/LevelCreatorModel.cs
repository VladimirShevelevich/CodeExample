using _App.Scripts.Root.Game.LevelsCreator.Reactive;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;

namespace _App.Scripts.Root.Game.LevelsCreator
{
    public class LevelCreatorModel : BaseDisposable
    {
        public struct Ctx
        {
            public ReactiveEvent<int> LoadLevelByIndex;
            public LevelLoadReactive LevelLoadReactive;
            public int LevelsAmount;
        }

        private readonly Ctx _ctx;

        public LevelCreatorModel(Ctx ctx)
        {
            _ctx = ctx;
        }
    }
}