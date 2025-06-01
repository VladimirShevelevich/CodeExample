using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.LevelUI.UpgradePopup
{
    public class UpgradePopupViewReactive : BaseDisposable
    {
        public readonly ReactiveTrigger OnCloseClicked = new();
        public readonly ReactiveTrigger OnHidden = new();
        public readonly ReactiveTrigger HideTrigger = new();

        public UpgradePopupViewReactive()
        {
            AddDisposable(OnCloseClicked);
            AddDisposable(OnHidden);
            AddDisposable(HideTrigger);
        }
    }
}