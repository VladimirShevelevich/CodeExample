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
            AddDisposable(_ctx.ViewReactive.CurrentPosition.Subscribe(HandlePositionChange));
        }

        private void HandlePositionChange(Vector3 position)
        {
            if (_ctx.LevelStateReactive.CurrentState.Value != LevelEntity.LevelState.Play)
                return;

            _ctx.ViewReactive.TargetRotation.Value = position.x > 0 ? 30 : -30;
        }

        private void EveryUpdate()
        {
            SetTargetVelocity();
        }

        private void SetTargetVelocity()
        {
            if (_ctx.LevelStateReactive.CurrentState.Value != LevelEntity.LevelState.Play)
            {
                _ctx.ViewReactive.TargetMoveVelocity.Value = Vector2.zero;
                return;
            }

            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");
            var velocity = new Vector2(horizontalInput, verticalInput) * _ctx.PlayerBarContent.MoveSpeed;
            _ctx.ViewReactive.TargetMoveVelocity.Value = velocity;
        }
    }
}