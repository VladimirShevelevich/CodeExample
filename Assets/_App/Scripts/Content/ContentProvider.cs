using _App.Scripts.Root.Game.LevelsCreator.Level;
using UnityEngine;

namespace _App.Scripts.Content
{
    [CreateAssetMenu(fileName = "ContentProvider", menuName = "Content/ContentProvider")]
    public class ContentProvider : ScriptableObject
    {
        [field: SerializeField] public BallsSpawnContent BallsSpawnContent;
        [field: SerializeField] public GemsContent GemsContent;
        [field: SerializeField] public UiContent UiContent;
        [field: SerializeField] public StatsContent StatsContent;
        [field: SerializeField] public PlayerBarContent PlayerBarContent;
        [field: SerializeField] public LevelConfig[] Levels { get; private set; }
    }

}