using _App.Scripts.Content;
using _App.Scripts.Root.Game;
using _App.Scripts.Tools.Core;
using UnityEngine;

namespace _App.Scripts.Root
{
    public class RootEntity : BaseEntity
    {
        public struct Ctx
        {
            public ContentProvider ContentProvider;
            public Transform UiCanvas;
        }
        
        private readonly Ctx _ctx;

        public RootEntity(Ctx ctx, Container parentContainer) : base(parentContainer)
        {
            _ctx = ctx;
            Container.Register<ContentProvider>(_ctx.ContentProvider);
            
            CreateGameEntity();
        }

        private void CreateGameEntity()
        {
            AddDisposable(new GameEntity(new GameEntity.Ctx
            {
                Canvas = _ctx.UiCanvas
            }, 
                Container));
        }
    }
}