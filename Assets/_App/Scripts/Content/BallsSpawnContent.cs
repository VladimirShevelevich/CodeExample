using System;
using System.Collections.Generic;
using System.Linq;
using _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator;
using _App.Scripts.Root.Game.LevelsCreator.Level.BallsCreator.Ball;
using _App.Scripts.Tools.Common;
using UnityEngine;

namespace _App.Scripts.Content
{
    [CreateAssetMenu(fileName = "BallsSpawn", menuName = "Content/BallsSpawn", order = 0)]
    public class BallsSpawnContent : ScriptableObject
    {
        [field: SerializeField] public float SpawnInterval { get; private set; }
        [field: SerializeField] public BallsSpawnArea SpawnArea { get; private set; }
        [field: SerializeField] public float SpecialBallChance { get; private set; }
        [field: SerializeField] private List<CustomKeyValuePair<BallType, BallInfo>> BallInfos;

        public BallInfo GetBallInfoByType(BallType ballType)
        {
            if (BallInfos.All(x => x.Key != ballType))
            {
                throw new InvalidOperationException($"Ball type {ballType} hasn't been found in BallDatas list");
            }
            
            return BallInfos.First(x => x.Key == ballType).Value;
        }
    }
    
    [Serializable]
    public struct BallInfo
    {
        public BallView Prefab;
        public float LifeTime;
        public int ScoreReward;
    }

    [Serializable]
    public struct BallsSpawnArea
    {
        public float WightRange;
        public float Height;
    }
}