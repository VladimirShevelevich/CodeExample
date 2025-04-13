using _App.Scripts.Content;
using _App.Scripts.Root.Game;
using _App.Scripts.Tools.Core;
using UnityEngine;

namespace _App.Scripts.Root
{
    public class RootEntity : BaseEntity<RootEntity.Ctx>
    {
        public struct Ctx
        {
            public ContentProvider ContentProvider;
            public Transform UiCanvas;
        }
        
        protected override void Initialize()
        {
            Container.Register(Context.ContentProvider);
            
            CreateGameEntity();
        }

        private void CreateGameEntity()
        {
            CreateEntity<GameEntity, GameEntity.Ctx>(new GameEntity.Ctx());
        }
    }
}