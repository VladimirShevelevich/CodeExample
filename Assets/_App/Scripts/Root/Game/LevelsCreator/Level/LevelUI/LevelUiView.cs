using System;
using _App.Scripts.Root.Game.LevelsCreator.Level.Reactive;
using TMPro;
using UniRx;
using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.LevelUI
{
    public class LevelUiView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _timerText;
        
        public struct Ctx
        {
            public ScoresReactive ScoresReactive;
            public LevelTimerReactive LevelTimerReactive;
        }

        private Ctx _ctx;

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
            _ctx.ScoresReactive.CurrentScore.Subscribe(SetScoreText).AddTo(this);
            _ctx.LevelTimerReactive.TimeLeft.Subscribe(SetTimerText).AddTo(this);
        }

        private void SetScoreText(int score)
        {
            _scoreText.text = $"Score: {score}";
        }
        
        private void SetTimerText(TimeSpan time)
        {
            _timerText.text = time.ToString(@"mm\:ss");
        }
    }
}