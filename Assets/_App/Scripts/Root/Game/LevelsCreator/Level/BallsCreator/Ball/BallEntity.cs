using _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator.Data;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Disposables;
using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator.Ball
{
    public class BallEntity : BaseEntity<BallEntity.Ctx>
    {
        public struct Ctx
        {
            public CreateBallData CreateBallData;
        }

        protected override void Initialize()
        {
            CreateView();
        }

        private void CreateView()
        {
            var prefab = Context.CreateBallData.BallInfo.Prefab;
            var view = Object.Instantiate(prefab);
            view.SetCtx(new BallView.Ctx());
            AddDisposable(new GameObjectDisposer(view.gameObject));
        }
    }
}