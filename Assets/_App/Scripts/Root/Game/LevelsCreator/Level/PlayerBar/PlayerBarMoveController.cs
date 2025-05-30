using _App.Scripts.Content;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Tools.Core;
using UniRx;
using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.PlayerBar
{
    public class PlayerBarMoveController : BaseEntity
    {
        public struct Ctx
        {
            public LevelStateReactive LevelStateReactive;
            public PlayerBarViewReactive ViewReactive;
            public PlayerBarContent PlayerBarContent;
        }

        private readonly Ctx _ctx; 
    
        public PlayerBarMoveController(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
            AddDisposable(Observable.EveryUpdate().Subscribe(_=> EveryUpdate()));
        }

        private void EveryUpdate()
        {
            if (_ctx.LevelStateReactive.CurrentState.Value != LevelEntity.LevelState.Play)
                _ctx.ViewReactive.Velocity.Value = Vector2.zero;

            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");
            var velocity = new Vector2(horizontalInput, verticalInput) * _ctx.PlayerBarContent.MoveSpeed * Time.deltaTime;
            _ctx.ViewReactive.Velocity.Value = velocity;
        }
    }
}