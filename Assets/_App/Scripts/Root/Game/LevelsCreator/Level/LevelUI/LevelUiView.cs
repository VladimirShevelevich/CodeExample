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
        
        [SerializeField] private GameObject _startingView;
        [SerializeField] private GameObject _playingView;
        [SerializeField] private GameObject _winView;
        [SerializeField] private GameObject _failingView;
        
        public struct Ctx
        {
            public ScoresReactive ScoresReactive;
            public LevelLoadReactive LevelLoadReactive;
            public IReadOnlyLevelTimeReactive LevelTimeReactive;
            public IReadOnlyLevelStateReactive LevelStateReactive;
            public int ScoreGoal;
            public int LevelIndex;
        }

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            _ctx.LevelStateReactive.ICurrentState.Subscribe(SetPlayStateView).AddTo(this);
            _ctx.ScoresReactive.CurrentScore.Subscribe(SetScoreText).AddTo(this);
            _ctx.LevelTimeReactive.ITimeLeft.Subscribe(SetTimerText).AddTo(this);
            
            _playButton.OnClickAsObservable().Subscribe(_=> OnPlayClick()).AddTo(this);
            _nextLevelButton.OnClickAsObservable().Subscribe(_=> OnNextLevelClick()).AddTo(this);
            _restartButton.OnClickAsObservable().Subscribe(_=> OnRestartClick()).AddTo(this);
            
            SetLevelIndexName();
        }

        private void SetLevelIndexName()
        {
            _levelIndexText.text = $"Level: {_ctx.LevelIndex + 1}";
        }

        private void SetPlayStateView(LevelEntity.LevelState levelState)
        {
            _startingView.SetActive(levelState == LevelEntity.LevelState.Start);
            _playingView.SetActive(levelState == LevelEntity.LevelState.Play);
            _winView.SetActive(levelState == LevelEntity.LevelState.Win);
            _failingView.SetActive(levelState == LevelEntity.LevelState.Fail);
        }

        private void OnPlayClick()
        {
            _ctx.LevelStateReactive.IPlayTrigger.Notify();
        }

        private void OnNextLevelClick()
        {
            _ctx.LevelLoadReactive.NextLevelTrigger.Notify();
        }

        private void OnRestartClick()
        {
            _ctx.LevelLoadReactive.RestartTrigger.Notify();
        }

        private void SetScoreText(int score)
        {
            _scoreText.text = $"Score: {score}/{_ctx.ScoreGoal}";
        }
        
        private void SetTimerText(TimeSpan time)
        {
            _timerText.text = time.ToString(@"mm\:ss");
        }
    }
}