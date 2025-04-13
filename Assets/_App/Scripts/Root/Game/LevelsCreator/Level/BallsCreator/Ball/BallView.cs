using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator.Ball
{
    public class BallView : MonoBehaviour
    {
        public struct Ctx
        {
            public BallViewReactive BallViewReactive;
        }

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
        }
    }
}