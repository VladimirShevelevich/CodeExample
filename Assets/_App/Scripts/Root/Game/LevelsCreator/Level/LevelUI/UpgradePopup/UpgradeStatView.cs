using _App.Scripts.Root.Game.UpgradeService;
using UniRx;
using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.LevelUI.UpgradePopup
{
    public class UpgradeStatView : MonoBehaviour
    {
        [SerializeField] private StatsServiceEntity.StatType _statType;
        [SerializeField] private GameObject[] _statUpgradedCells;
        
        public struct Ctx
        {
            public UpgradePopupViewReactive UpgradePopupViewReactive;
        }

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            SubscribeForStatChange();
            UpdateStat(_ctx.UpgradePopupViewReactive.StatLevels[_statType]);
        }

        private void SubscribeForStatChange()
        {
            _ctx.UpgradePopupViewReactive.StatLevels.ObserveAdd().
                Where(x => x.Key == _statType).
                Subscribe(evt => UpdateStat(evt.Value)).
                AddTo(this);

            _ctx.UpgradePopupViewReactive.StatLevels.ObserveReplace().
                Where(x => x.Key == _statType).
                Subscribe(evt => UpdateStat(evt.NewValue)).
                AddTo(this);
        }

        private void UpdateStat(int level)
        {
            for (var i = 0; i < _statUpgradedCells.Length; i++)
            {
                _statUpgradedCells[i].SetActive(level >= i);
            }
        }
    }
}