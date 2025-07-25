﻿using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator.Ball
{
    public class BallView : MonoBehaviour
    {
        [SerializeField] private Transform _model;
        
        public struct Ctx
        {
            public BallViewReactive BallViewReactive;
        }

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            _ctx.BallViewReactive.HideTrigger.Subscribe(Hide).AddTo(this);
            
            PlayShowVFX();
        }

        private void PlayShowVFX()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 0.25f).
                SetLink(gameObject);
        }

        private void Hide()
        {
            _model.DOScale(0, 0.1f).
                OnComplete(()=> gameObject.SetActive(false)).
                SetLink(gameObject);
        }
    }
}