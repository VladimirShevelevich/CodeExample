using System.Collections.Generic;
using _App.Scripts.Root.Game.UpgradeService;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;
using UniRx;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.LevelUI.UpgradePopup
{
    public class UpgradePopupViewReactive : BaseDisposable
    {
        public readonly ReactiveTrigger OnCloseClicked = new();
        public readonly ReactiveEvent<StatsServiceEntity.StatType> OnIncreaseClicked = new();
        public readonly ReactiveTrigger OnApplyClicked = new();
        public readonly ReactiveTrigger OnResetClicked = new();
        public readonly ReactiveTrigger OnHidden = new();
        public readonly ReactiveTrigger HideTrigger = new();
        public readonly ReactiveProperty<bool> IsApplyButtonEnabled = new();
        public readonly ReactiveDictionary<StatsServiceEntity.StatType, int> StatLevels = new(
            new Dictionary<StatsServiceEntity.StatType, int>());
        
        public UpgradePopupViewReactive()
        {
            AddDisposable(OnCloseClicked);
            AddDisposable(OnHidden);
            AddDisposable(HideTrigger);
            AddDisposable(StatLevels);
            AddDisposable(OnIncreaseClicked);
            AddDisposable(OnApplyClicked);
            AddDisposable(OnResetClicked);
            AddDisposable(IsApplyButtonEnabled);
        }
    }
}