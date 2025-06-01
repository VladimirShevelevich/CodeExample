using System.Collections.Generic;
using _App.Scripts.Tools.Core;
using UniRx;

namespace _App.Scripts.Root.Game.UpgradeService
{
    public class StatsReactive : BaseDisposable
    {
        public readonly ReactiveDictionary<StatsServiceEntity.StatType, int> StatLevels = new(
            new Dictionary<StatsServiceEntity.StatType, int>());

        public StatsReactive()
        {
            AddDisposable(StatLevels);
        }
    }
}