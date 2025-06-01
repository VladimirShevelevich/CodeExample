using System.Linq;
using _App.Scripts.Content;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Root.Game.UpgradeService;
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
            public StatsReactive StatsReactive;
            public PlayerBarViewReactive ViewReactive;
            public PlayerBarContent PlayerBarContent;
            public StatsContent StatsContent;
        }

        private readonly Ctx _ctx;
        private float _speedWithStat;
    
        public PlayerBarMoveController(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
            AddDisposable(Observable.EveryUpdate().Subscribe(_=> EveryUpdate()));
            AddDisposable(_ctx.ViewReactive.CurrentPosition.Subscribe(HandlePositionChange));
            
            SetSpeedWithStat();
        }

        private void SetSpeedWithStat()
        {
            var currentStat = _ctx.StatsReactive.StatLevels[StatsServiceEntity.StatType.Speed];
            var statModifiers = _ctx.StatsContent.StatModifiersByLevel.First(x => x.Key == StatsServiceEntity.StatType.Speed).Value;
            _speedWithStat = _ctx.PlayerBarContent.MoveSpeed * statModifiers[currentStat];
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
            var velocity = new Vector2(horizontalInput, verticalInput) * _speedWithStat;
            _ctx.ViewReactive.TargetMoveVelocity.Value = velocity;
        }
    }
}