using UniRx;
using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.PlayerBar
{
    public class PlayerBarView : MonoBehaviour
    {
        public struct Ctx
        {
            public PlayerBarViewReactive ViewReactive;
        }

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            _ctx.ViewReactive.Velocity.Subscribe(ApplyVelocity).
                AddTo(this);
        }

        private void ApplyVelocity(Vector2 velocity)
        {
            transform.Translate(new Vector3(velocity.x, velocity.y));;
            Debug.Log(velocity);
        }
    }
}