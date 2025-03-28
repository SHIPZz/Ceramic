using UnityEngine;

namespace CodeBase.Infrastructure.Providers
{
    public class LevelProvider : ILevelProvider
    {
        public Transform CubeParent { get;  set; }

        public GameObject Prefab { get; set; }
    }
}