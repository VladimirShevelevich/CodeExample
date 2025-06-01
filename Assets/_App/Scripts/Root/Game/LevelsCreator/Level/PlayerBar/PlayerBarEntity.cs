using _App.Scripts.Content;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Root.Game.UpgradeService;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Disposables;
using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.PlayerBar
{
    public class PlayerBarEntity : BaseEntity
    {
        public struct Ctx
        {
            public PlayerBarContent PlayerBarContent;
        }

        private readonly Ctx _ctx;
        private readonly PlayerBarViewReactive _viewReactive = new();
        
        public PlayerBarEntity(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
            AddDisposable(_viewReactive);
            
            CreateMoveController();
            CreateView();
        }

        private void CreateMoveController()
        {
            var ctx = new PlayerBarMoveController.Ctx
            {
                PlayerBarContent = _ctx.PlayerBarContent,
                ViewReactive = _viewReactive,
                LevelStateReactive = Container.Resolve<LevelStateReactive>(),
                StatsReactive = Container.Resolve<StatsReactive>(),
                StatsContent = Container.Resolve<ContentProvider>().StatsContent
            };
            AddDisposable(new PlayerBarMoveController(ctx, Container));
        }
        
        private void CreateView()
        {
            var prefab = _ctx.PlayerBarContent.Prefab;
            var view = Object.Instantiate(prefab);
            view.SetCtx(new PlayerBarView.Ctx
            {
                ViewReactive = _viewReactive,
                PlayerBarContent = Container.Resolve<ContentProvider>().PlayerBarContent
            });
            AddDisposable(new GameObjectDisposer(view.gameObject));
        }
    }        
}