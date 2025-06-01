using System;
using _App.Scripts.Tools.Core;
using _App.Scripts.Tools.Reactive;
using UniRx;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.LevelUI
{
    public class LevelUiViewReactive : BaseDisposable
    {
        public readonly ReactiveProperty<LevelEntity.LevelState> CurrentState = new();
        public readonly ReactiveProperty<int> CurrentScore = new ();
        public readonly ReactiveProperty<int> ScoreGoal = new ();
        public readonly ReactiveProperty<TimeSpan> TimeLeft = new();
        public readonly ReactiveProperty<int> LevelIndex = new();

        public readonly ReactiveTrigger OnPlayButtonClicked = new();
        public readonly ReactiveTrigger OnNextLevelClicked = new();
        public readonly ReactiveTrigger OnRestartClicked = new();
        public readonly ReactiveTrigger OnUpgradeClicked = new();

        public LevelUiViewReactive()
        {
            AddDisposable(CurrentState);
            AddDisposable(CurrentScore);
            AddDisposable(ScoreGoal);
            AddDisposable(TimeLeft);
            AddDisposable(OnPlayButtonClicked);
            AddDisposable(OnNextLevelClicked);
            AddDisposable(OnRestartClicked);
            AddDisposable(LevelIndex);
            AddDisposable(OnUpgradeClicked);
        }
    }
}