using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator.Ball
{
    public class BallView : MonoBehaviour
    {
        public struct Ctx
        {
        }

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
        }
    }
}