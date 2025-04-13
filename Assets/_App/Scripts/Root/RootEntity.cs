using _App.Scripts.Content;
using _App.Scripts.Root.Game;
using _App.Scripts.Tools.Core;
using UnityEngine;

namespace _App.Scripts.Root
{
    public class RootEntity : BaseEntity<RootEntity.Context>
    {
        public struct Context
        {
            public ContentProvider ContentProvider;
            public Transform UiCanvas;
        }
        
        protected override void Initialize()
        {
            CreateGameEntity();
        }

        private void CreateGameEntity()
        {
            CreateEntity<GameEntity, GameEntity.Context>(new GameEntity.Context());
        }
    }
}