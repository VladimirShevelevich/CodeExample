using _App.Scripts.Root.Game.LevelsCreator.Level.PlayerBar;
using UnityEngine;

namespace _App.Scripts.Content
{
    [CreateAssetMenu(fileName = "PlayerBarContent", menuName = "Content/PlayerBar")]
    public class PlayerBarContent : ScriptableObject
    {
        [field: SerializeField] public PlayerBarView Prefab { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float TiltModifier { get; private set; }
    }
}