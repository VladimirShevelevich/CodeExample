using _App.Scripts.Content;
using UniRx;
using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.PlayerBar
{
    public class PlayerBarView : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidBody;
        
        public struct Ctx
        {
            public PlayerBarViewReactive ViewReactive;
            public PlayerBarContent PlayerBarContent;
        }

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            Observable.EveryFixedUpdate().Subscribe(_ => EveryFixedUpdate()).
                AddTo(this);
            
            ApplyScaleModifier();
        }

        private void ApplyScaleModifier()
        {
            transform.localScale = new Vector3(transform.localScale.x * _ctx.ViewReactive.ScaleModifier.Value,
                transform.localScale.y, transform.localScale.z);
        }
        
        private void EveryFixedUpdate()
        {
            HandleVelocity();
            HandleTilt();
        }

        private void HandleVelocity()
        {
            var targetVelocity = _ctx.ViewReactive.TargetMoveVelocity.Value;
            _rigidBody.velocity = new Vector3(targetVelocity.x, targetVelocity.y);
        }

        private void HandleTilt()
        {
            var tilt = transform.position.x * _ctx.PlayerBarContent.TiltModifier;
            _rigidBody.MoveRotation(Quaternion.Euler(0, 0, tilt));
        }

        private void ApplyVelocity(Vector2 velocity)
        {
            _rigidBody.velocity = new Vector3(velocity.x, velocity.y);
        }
    }
}