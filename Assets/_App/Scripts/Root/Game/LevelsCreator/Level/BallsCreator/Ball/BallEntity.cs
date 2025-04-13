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

        private readonly BallViewReactive _ballViewReactive = new();
        
        protected override void Initialize()
        {
            AddDisposable(_ballViewReactive);
            
            CreateView();
        }

        private void CreateView()
        {
            var prefab = Context.CreateBallData.BallInfo.Prefab;
            var position = new Vector3(Context.CreateBallData.Position.x, 0, Context.CreateBallData.Position.y);
            var view = Object.Instantiate(prefab, position, Quaternion.identity);
            view.SetCtx(new BallView.Ctx
            {
                BallViewReactive = _ballViewReactive
            });
            AddDisposable(new GameObjectDisposer(view.gameObject));
        }
    }
}