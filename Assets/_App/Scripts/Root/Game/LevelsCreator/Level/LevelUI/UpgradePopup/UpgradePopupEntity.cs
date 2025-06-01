using _App.Scripts.Tools.Core;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.LevelUI.UpgradePopup
{
    public class UpgradePopupEntity : BaseEntity
    {
        public struct Ctx
        {

        } 
        
        private readonly Ctx _ctx; 
        
        public UpgradePopupEntity(Ctx context, Container parentContainer) : base(parentContainer)
        {
            _ctx = context;
        }
    }
}