using _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator;
using _App.Scripts.Tools.Core;

namespace _App.Scripts.Root.Game.LevelsCreator.Level
{
    public class LevelEntity : BaseEntity<LevelEntity.Ctx>
    {
        public struct Ctx
        {
            public LevelConfig LevelConfig;
        }

        protected override void Initialize()
        {
            CreateBallsCreator();
        }

        private void CreateBallsCreator()
        {
            CreateEntity<BallsCreatorEntity, BallsCreatorEntity.Ctx>(new BallsCreatorEntity.Ctx());
        }
    }
}