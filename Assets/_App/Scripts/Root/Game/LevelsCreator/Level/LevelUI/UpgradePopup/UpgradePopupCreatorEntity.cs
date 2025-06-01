using _App.Scripts.Content;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;
using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.LevelUI.UpgradePopup
{
    public class UpgradePopupCreatorEntity : BaseEntity
    {
        public struct Ctx
        {
            public LevelUiViewReactive LevelUiViewReactive;
            public Transform UiCanvas;
        } 
        
        private readonly Ctx _ctx;
        
        public UpgradePopupCreatorEntity(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
            AddDisposable(_ctx.LevelUiViewReactive.OnUpgradeClicked.Subscribe(CreatePopup));
        }

        private void CreatePopup()
        {
            var onPopupHidden = new ReactiveTrigger();
            AddDisposable(onPopupHidden);
            var ctx = new UpgradePopupEntity.Ctx
            {
                UpgradeContent = Container.Resolve<ContentProvider>().UpgradeContent,
                UiCanvas = _ctx.UiCanvas,
                OnHidden = onPopupHidden
            };
            var entity = new UpgradePopupEntity(ctx, Container);
            AddDisposable(entity);
            //AddDisposable(onPopupHidden.Subscribe(entity.Dispose));
        }
    }
}