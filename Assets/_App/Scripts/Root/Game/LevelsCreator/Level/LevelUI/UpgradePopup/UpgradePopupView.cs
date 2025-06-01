using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.LevelUI.UpgradePopup
{
    public class UpgradePopupView : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        
        public struct Ctx
        {
            public UpgradePopupViewReactive ViewReactive;
        }

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            _ctx.ViewReactive.HideTrigger.Subscribe(Hide).AddTo(this);
            
            _closeButton.OnClickAsObservable().Subscribe(_=> OnCloseClick()).AddTo(this);
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
    }
}