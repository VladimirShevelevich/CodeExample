using _App.Scripts.Tools.Core;

namespace _App.Scripts.Root.Game.UpgradeService
{
    public class StatsServiceEntity : BaseEntity
    {
        public enum StatType
        {
            Speed,
            Scale
        }
        
        public struct Ctx
        {
            public StatsReactive StatsReactive;
        }

        private readonly Ctx _ctx; 
        
        public StatsServiceEntity(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
            InitializeStats();
        }

        private void InitializeStats()
        {
            _ctx.StatsReactive.StatLevels.Add(StatType.Speed, 0);
            _ctx.StatsReactive.StatLevels.Add(StatType.Scale, 0);
        }
    }
}