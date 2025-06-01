using _App.Scripts.Content;
using _App.Scripts.Root.Game.LevelsCreator;
using _App.Scripts.Root.Game.UpgradeService;
using _App.Scripts.Tools.Core;
using UnityEngine;

namespace _App.Scripts.Root.Game
{
    public class GameEntity : BaseEntity
    {
        public struct Ctx
        {
            public Transform Canvas;
        }
        
        private readonly Ctx _ctx;
        private readonly StatsReactive _statsReactive = new();

        public GameEntity(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
            AddDisposable(_statsReactive);
            Container.Register<StatsReactive>(_statsReactive);

            CreateStatsService();
            CreateLevelCreator();
        }

        private void CreateStatsService()
        {
            var ctx = new StatsServiceEntity.Ctx
            {
                StatsReactive = _statsReactive
            };
            AddDisposable(new StatsServiceEntity(ctx, Container));
        }

        private void CreateLevelCreator()
        {
            AddDisposable(new LevelCreatorEntity(
                new LevelCreatorEntity.Ctx
                {
                    LevelsConfigs = Container.Resolve<ContentProvider>().Levels,
                    Canvas = _ctx.Canvas
                },
                Container));
        }
    }
}