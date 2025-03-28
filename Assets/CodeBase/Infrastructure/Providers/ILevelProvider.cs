using CodeBase.Gameplay;
using UnityEngine;

namespace CodeBase.Infrastructure.Providers
{
    public interface ILevelProvider
    {
        Transform CubeParent { get; set; }
        
        CubeVisualizationView CubePrefab { get; set; }
    }
}