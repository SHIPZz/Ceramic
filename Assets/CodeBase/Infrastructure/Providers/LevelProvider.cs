using CodeBase.Gameplay;
using UnityEngine;

namespace CodeBase.Infrastructure.Providers
{
    public class LevelProvider : ILevelProvider
    {
        public Transform CubeParent { get;  set; }

        public CubeVisualizationView CubePrefab { get; set; }
    }
}