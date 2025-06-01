using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.LevelUI.UpgradePopup
{
    public class UpgradePopupView : MonoBehaviour
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