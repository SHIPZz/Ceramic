using CodeBase.Infrastructure.Providers;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Initializers
{
    public class LevelInitializer : MonoBehaviour, IInitializable
    {
        [field: SerializeField] public Transform CubeParent { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
        
        private ILevelProvider _levelProvider;

        [Inject]
        private void Construct(ILevelProvider levelProvider)
        {
            _levelProvider = levelProvider;
        }

        public void Initialize()
        {
            _levelProvider.CubeParent = CubeParent;
            _levelProvider.Prefab = Prefab;
        }
    }
}