using System.Collections.Generic;
using _App.Scripts.Root.Game.LevelsCreator.Level.LevelUI.UpgradePopup;
using _App.Scripts.Root.Game.UpgradeService;
using _App.Scripts.Tools.Common;
using UnityEngine;

namespace _App.Scripts.Content
{
    [CreateAssetMenu(fileName = "UpgradeContent", menuName = "Content/Upgrade", order = 0)]
    public class StatsContent : ScriptableObject
    {
        [field: SerializeField] public UpgradePopupView UpgradePopupPrefab { get; private set; }
        [field: SerializeField]
        public List<CustomKeyValuePair<StatsServiceEntity.StatType, List<float>>> StatModifiersByLevel
        {
            get;
            private set;
        }
    }
}