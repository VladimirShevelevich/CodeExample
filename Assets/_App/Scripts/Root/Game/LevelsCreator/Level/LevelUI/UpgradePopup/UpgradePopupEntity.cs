using _App.Scripts.Content;
using _App.Scripts.Root.Game.UpgradeService;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Disposables;
using _App.Scripts.Tools.Reactive;
using UnityEngine;

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

            SetStatsView();
            CreateView();
        }

        private void SetStatsView()
        {
            foreach (var statLevel in _ctx.StatsReactive.StatLevels)
            {
                _viewReactive.StatLevels.Add(statLevel.Key, statLevel.Value);
            }
        }

        private void OnCloseClicked()
        {
            _viewReactive.HideTrigger.Notify();
        }

        private void OnHidden()
        {
            _ctx.OnHidden.Notify();
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