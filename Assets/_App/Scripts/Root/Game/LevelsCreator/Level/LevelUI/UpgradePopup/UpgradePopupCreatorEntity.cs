using _App.Scripts.Tools.Core;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.LevelUI.UpgradePopup
{
    public class UpgradePopupCreatorEntity : BaseEntity
    {
        public struct Ctx
        {

        } 
        
        private readonly Ctx _ctx; 
        
        public UpgradePopupCreatorEntity(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
        }

        private void CreatePopup()
        {
            var ctx = new UpgradePopupEntity.Ctx
            {

            };
            AddDisposable(new UpgradePopupEntity(ctx, Container));
        }
    }
}