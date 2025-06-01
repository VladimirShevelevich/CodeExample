using _App.Scripts.Root.Game.LevelsCreator.Level.LevelUI.UpgradePopup;
using UnityEngine;

namespace _App.Scripts.Content
{
    [CreateAssetMenu(fileName = "UpgradeContent", menuName = "Content/Upgrade", order = 0)]
    public class UpgradeContent : ScriptableObject
    {
        [field: SerializeField] public UpgradePopupView UpgradePopupPrefab { get; private set; }
    }
}