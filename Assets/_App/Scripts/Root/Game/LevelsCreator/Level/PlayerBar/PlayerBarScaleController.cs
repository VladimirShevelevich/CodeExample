using System.Linq;
using _App.Scripts.Content;
using _App.Scripts.Root.Game.UpgradeService;
using _App.Scripts.Tools.Core;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.PlayerBar
{
    public class PlayerBarScaleController : BaseEntity
    {
        public struct Ctx
        {
            public StatsReactive StatsReactive;
            public StatsContent StatsContent;
            public PlayerBarViewReactive ViewReactive;
        }

        private readonly Ctx _ctx;
        
        public PlayerBarScaleController(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
            SetScaleModifier();
        }

        private void SetScaleModifier()
        {
            var currentStat = _ctx.StatsReactive.StatLevels[StatsServiceEntity.StatType.Scale];
            var statModifiers = _ctx.StatsContent.StatModifiersByLevel.First(x => x.Key == StatsServiceEntity.StatType.Scale).Value;
            _ctx.ViewReactive.ScaleModifier.Value = statModifiers[currentStat];
        }
    }
}