using System.Linq;
using _App.Scripts.Content;
using _App.Scripts.Root.Game.UpgradeService;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Disposables;
using _App.Scripts.Tools.Reactive;
using UnityEngine;
using UniRx;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.LevelUI.UpgradePopup
{
    public class UpgradePopupEntity : BaseEntity
    {
        public struct Ctx
        {
            public StatsReactive StatsReactive;
            public ReactiveTrigger OnHidden;
            public UpgradeContent UpgradeContent;
            public Transform UiCanvas;
        } 
        
        private readonly Ctx _ctx;
        private readonly UpgradePopupViewReactive _viewReactive = new();
        
        public UpgradePopupEntity(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
            AddDisposable(_viewReactive);
            AddDisposable(_viewReactive.OnCloseClicked.Subscribe(OnCloseClicked));
            AddDisposable(_viewReactive.OnHidden.Subscribe(OnHidden));

            InitStatsView();
            UpdateIsApplyButtonEnabled();
            CreateView();
        }

        private void InitStatsView()
        {
            foreach (var statLevel in _ctx.StatsReactive.StatLevels)
            {
                _viewReactive.StatLevels.Add(statLevel.Key, statLevel.Value);
            }

            AddDisposable(_ctx.StatsReactive.StatLevels.ObserveReplace().Subscribe(evt =>
            {
                _viewReactive.StatLevels[evt.Key] = evt.NewValue;
            }));
            
            AddDisposable(_viewReactive.OnIncreaseClicked.SubscribeWithSkip(IncreaseStatView));
            AddDisposable(_viewReactive.OnApplyClicked.Subscribe(ApplyStats));
            AddDisposable(_viewReactive.OnResetClicked.Subscribe(ResetStatsViews));
        }

        private void IncreaseStatView(StatsServiceEntity.StatType statType)
        {
            _viewReactive.StatLevels[statType]++;
            UpdateIsApplyButtonEnabled();
        }

        private void OnCloseClicked()
        {
            _viewReactive.HideTrigger.Notify();
        }

        private void OnHidden()
        {
            _ctx.OnHidden.Notify();
        }

        private void ApplyStats()
        {
            var statTypes = _ctx.StatsReactive.StatLevels.Keys.ToArray();
            foreach (var statType in statTypes)
            {
                _ctx.StatsReactive.StatLevels[statType] = _viewReactive.StatLevels[statType];
            }
            UpdateIsApplyButtonEnabled();
        }

        private void ResetStatsViews()
        {
            foreach (var statLevel in _ctx.StatsReactive.StatLevels)
            {
                _viewReactive.StatLevels[statLevel.Key] = statLevel.Value;
            }
            UpdateIsApplyButtonEnabled();
        }

        private void UpdateIsApplyButtonEnabled()
        {
            var enabled = _ctx.StatsReactive.StatLevels.Any(x => x.Value != _viewReactive.StatLevels[x.Key]);
            _viewReactive.IsApplyButtonEnabled.Value = enabled;
        }

        private void CreateView()
        {
            var view = Object.Instantiate(_ctx.UpgradeContent.UpgradePopupPrefab, _ctx.UiCanvas);
            view.SetCtx(new UpgradePopupView.Ctx
            {
                ViewReactive = _viewReactive
            });
            AddDisposable(new GameObjectDisposer(view.gameObject));
        }
    }
}