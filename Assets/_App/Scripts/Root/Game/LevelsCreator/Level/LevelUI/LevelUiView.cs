using System;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
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
        
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _restartButton;
        
        [SerializeField] private GameObject _startingView;
        [SerializeField] private GameObject _playingView;
        
        public struct Ctx
        {
            public ScoresReactive ScoresReactive;
            public LevelTimeReactive LevelTimeReactive;
            public LevelUiTriggersReactive LevelUiTriggersReactive;
            public LevelStateReactive LevelStateReactive;
            public int ScoreGoal;
        }

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            _ctx.LevelStateReactive.CurrentState.Subscribe(SetPlayStateView).AddTo(this);
            _ctx.ScoresReactive.CurrentScore.Subscribe(SetScoreText).AddTo(this);
            _ctx.LevelTimeReactive.TimeLeft.Subscribe(SetTimerText).AddTo(this);
            
            _playButton.OnClickAsObservable().Subscribe(_=> OnPlayClick()).AddTo(this);
            //_nextLevelButton.OnClickAsObservable().Subscribe(_=> OnNextLevelClick()).AddTo(this);
            //_restartButton.OnClickAsObservable().Subscribe(_=> OnRestartClick()).AddTo(this);
        }

        private void SetPlayStateView(LevelEntity.LevelState levelState)
        {
            _startingView.SetActive(levelState == LevelEntity.LevelState.Start);
            _playingView.SetActive(levelState == LevelEntity.LevelState.Play);
        }

        private void OnPlayClick()
        {
            _ctx.LevelUiTriggersReactive.PlayTrigger.Notify();
        }

        private void OnNextLevelClick()
        {
            _ctx.LevelUiTriggersReactive.NextLevelTrigger.Notify();
        }

        private void OnRestartClick()
        {
            _ctx.LevelUiTriggersReactive.RestartTrigger.Notify();
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