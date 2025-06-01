using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.LevelUI.UpgradePopup
{
    public class UpgradePopupView : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _applyButton;
        [SerializeField] private Button _resetButton;
        [SerializeField] private UpgradeStatView[] _upgradeStatViews;
        
        public struct Ctx
        {
            public UpgradePopupViewReactive ViewReactive;
        }

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            InitStatViews();
            
            _ctx.ViewReactive.HideTrigger.Subscribe(Hide).AddTo(this);
            _ctx.ViewReactive.IsApplyButtonEnabled.Subscribe(OnApplyBtnEnabledChanged).AddTo(this);
            _closeButton.OnClickAsObservable().Subscribe(_=> OnCloseClick()).AddTo(this);
            _applyButton.OnClickAsObservable().Subscribe(_=> OnApplyClick()).AddTo(this);
            _resetButton.OnClickAsObservable().Subscribe(_=> OnResetClick()).AddTo(this);
        }

        private void InitStatViews()
        {
            foreach (var upgradeStatView in _upgradeStatViews)
            {
                upgradeStatView.SetCtx(new UpgradeStatView.Ctx
                {
                    UpgradePopupViewReactive = _ctx.ViewReactive
                });
            }
        }

        private void Hide()
        {
            transform.DOScaleX(0, 0.2f).
                OnComplete(OnHidden).
                SetLink(gameObject);
        }

        private void OnHidden()
        {
            _ctx.ViewReactive.OnHidden.Notify();
        }

        private void OnCloseClick()
        {
            _ctx.ViewReactive.OnCloseClicked.Notify();
        }

        private void OnApplyClick()
        {
            _ctx.ViewReactive.OnApplyClicked.Notify();
        }

        private void OnResetClick()
        {
            _ctx.ViewReactive.OnResetClicked.Notify();
        }

        private void OnApplyBtnEnabledChanged(bool enabled)
        {
            _applyButton.gameObject.SetActive(enabled);
        }
    }
}