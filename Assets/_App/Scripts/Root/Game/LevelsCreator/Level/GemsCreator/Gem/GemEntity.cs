using System;
using _App.Scripts.Content;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Disposables;
using _App.Scripts.Tools.Reactive;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.GemsCreator.Gem
{
    public class GemEntity : BaseEntity
    {
        public struct Ctx
        {
            public ReactiveEvent<IDisposable> OnGemHidden;
            public GemsContent GemsContent;
            public CreateGemData CreateGemData;
        }

        private readonly Ctx _ctx;
        private readonly GemViewReactive _gemViewReactive = new();
        
        public GemEntity(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
            AddDisposable(_gemViewReactive);
            
            CreateScoreController();
            CreateLifeTimeController();
            
            CreateView();
        }

        private void CreateScoreController()
        {
            var ctx = new GemScoreController.Ctx
            {
                GemsContent = _ctx.GemsContent,
                GemViewReactive = _gemViewReactive,
                ScoresReactive = Container.Resolve<ScoresReactive>()
            };
            AddDisposable(new GemScoreController(ctx, Container));
        }        
        
        private void CreateLifeTimeController()
        {
            var ctx = new GemLifeTimeController.Ctx
            {
                GemViewReactive = _gemViewReactive,
                OnGemHidden = _ctx.OnGemHidden
            };
            AddDisposable(new GemLifeTimeController(ctx, Container));
        }

        private void CreateView()
        {
            var prefab = _ctx.GemsContent.Prefab;
            var position = new Vector3(_ctx.CreateGemData.Position.x, _ctx.CreateGemData.Position.y, 0);
            var view = Object.Instantiate(prefab, position, prefab.transform.rotation);
            view.SetCtx(new GemView.Ctx
            {
                GemViewReactive = _gemViewReactive
            });
            AddDisposable(new GameObjectDisposer(view.gameObject));
        }
    }
}