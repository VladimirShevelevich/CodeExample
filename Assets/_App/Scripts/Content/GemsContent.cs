using System;
using _App.Scripts.Root.Game.LevelsCreator.Level.GemsCreator.Gem;
using UnityEngine;

namespace _App.Scripts.Content
{
    [CreateAssetMenu(fileName = "GemsContent", menuName = "Content/Gems", order = 0)]
    public class GemsContent : ScriptableObject
    {
        [field: SerializeField] public GemsSpawnArea SpawnArea { get; private set; }
        [field: SerializeField] public GemView Prefab { get; private set; }
        [field: SerializeField] public int Reward { get; private set; }
    }

    [Serializable]
    public struct GemsSpawnArea
    {
        public float WightRange;
        public float HeightRange;
    }
}