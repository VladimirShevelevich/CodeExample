using _App.Scripts.Tools.Core;
using UniRx;
using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator.Level.PlayerBar
{
    public class PlayerBarViewReactive : BaseDisposable
    {
        public readonly ReactiveProperty<Vector2> TargetMoveVelocity = new();
        public readonly ReactiveProperty<float> ScaleModifier = new();

        public PlayerBarViewReactive()
        {
            AddDisposable(TargetMoveVelocity);
            AddDisposable(ScaleModifier);
        }
    }
}