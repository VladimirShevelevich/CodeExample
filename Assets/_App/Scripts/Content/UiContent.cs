using _App.Scripts.Root.Game.LevelsCreator.Level.LevelUI;
using UnityEngine;

namespace _App.Scripts.Content
{
    [CreateAssetMenu(fileName = "UiContent", menuName = "Content/UI", order = 0)]
    public class UiContent : ScriptableObject
    {
        [field: SerializeField] public LevelUiView LevelUiPrefab { get; private set; }
    }
}