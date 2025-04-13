using _App.Scripts.Content;
using _App.Scripts.Root.Game.LevelsCreator;
using _App.Scripts.Tools.Core;

namespace _App.Scripts.Root.Game
{
    public class GameEntity : BaseEntity<GameEntity.Ctx>
    {
        public struct Ctx { }

        protected override void Initialize()
        {
            CreateLevelCreator();
        }

        private void CreateLevelCreator()
        {
            CreateEntity<LevelCreatorEntity, LevelCreatorEntity.Ctx>(new LevelCreatorEntity.Ctx
            {
                LevelsConfigs = Container.Resolve<ContentProvider>().Levels
            });
        }
    }
}