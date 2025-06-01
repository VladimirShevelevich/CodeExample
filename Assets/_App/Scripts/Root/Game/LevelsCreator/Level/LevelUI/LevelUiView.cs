using System;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using _App.Scripts.Root.Game.LevelsCreator.Reactive;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.LevelUI
{
    public class LevelUiView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private TMP_Text _levelIndexText;
        
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _upgradeButton;
        
        [SerializeField] private GameObject _startingView;
        [SerializeField] private GameObject _playingView;
        [SerializeField] private GameObject _winView;
        [SerializeField] private GameObject _failingView;
        
        public struct Ctx
        {
            public LevelUiViewReactive ViewReactive;
        }

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            _ctx.ViewReactive.CurrentState.Subscribe(SetLevelPlayStateView).AddTo(this);
            _ctx.ViewReactive.CurrentScore.Subscribe(x => SetScoreText()).AddTo(this);
            _ctx.ViewReactive.TimeLeft.Subscribe(SetTimerText).AddTo(this);
            _ctx.ViewReactive.LevelIndex.Subscribe(SetLevelIndexName).AddTo(this);
            
            _playButton.OnClickAsObservable().Subscribe(_=> OnPlayClick()).AddTo(this);
            _nextLevelButton.OnClickAsObservable().Subscribe(_=> OnNextLevelClick()).AddTo(this);
            _restartButton.OnClickAsObservable().Subscribe(_=> OnRestartClick()).AddTo(this);
            _upgradeButton.OnClickAsObservable().Subscribe(_=> OnUpgradeClick()).AddTo(this);
        }

        private void SetLevelIndexName(int levelIndex)
        {
            _levelIndexText.text = $"Level: {levelIndex}";
        }

        private void SetLevelPlayStateView(LevelEntity.LevelState levelState)
        {
            _startingView.SetActive(levelState == LevelEntity.LevelState.Start);
            _playingView.SetActive(levelState == LevelEntity.LevelState.Play);
            _winView.SetActive(levelState == LevelEntity.LevelState.Win);
            _failingView.SetActive(levelState == LevelEntity.LevelState.Fail);
        }

        private void OnPlayClick()
        {
            _ctx.ViewReactive.OnPlayButtonClicked.Notify();
        }

        private void OnNextLevelClick()
        {
            _ctx.ViewReactive.OnNextLevelClicked.Notify();
        }

        private void OnRestartClick()
        {
            _ctx.ViewReactive.OnRestartClicked.Notify();
        }
        
        private void OnUpgradeClick()
        {
            _ctx.ViewReactive.OnUpgradeClicked.Notify();
        }

        private void SetScoreText()
        {
            _scoreText.text = $"Score: {_ctx.ViewReactive.CurrentScore}/{_ctx.ViewReactive.ScoreGoal}";
        }
        
        private void SetTimerText(TimeSpan time)
        {
            _timerText.text = time.ToString(@"mm\:ss");
        }
    }
}