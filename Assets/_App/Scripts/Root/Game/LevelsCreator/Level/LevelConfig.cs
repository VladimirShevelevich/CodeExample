using UnityEngine;

namespace _App.Scripts.Root.Game.LevelsCreator.Level
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Content/Level", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public int TimeInSeconds { get; private set; }
    }
}