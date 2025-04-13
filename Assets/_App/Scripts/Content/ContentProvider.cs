using _App.Scripts.Root.Game.LevelsCreator.Level;
using UnityEngine;

namespace _App.Scripts.Content
{
    [CreateAssetMenu(fileName = "ContentProvider", menuName = "Content/ContentProvider")]
    public class ContentProvider : ScriptableObject
    {
        [field: SerializeField] public BallsSpawnContent BallsSpawnContent;
        [field: SerializeField] public LevelConfig[] Levels { get; private set; }
    }

}