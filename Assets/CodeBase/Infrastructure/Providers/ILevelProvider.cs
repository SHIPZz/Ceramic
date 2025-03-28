using UnityEngine;

namespace CodeBase.Infrastructure.Providers
{
    public interface ILevelProvider
    {
        Transform CubeParent { get; set; }
        
        GameObject Prefab { get; set; }
    }
}