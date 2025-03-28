using System.Collections.Generic;
using CodeBase.Constants;
using CodeBase.Gameplay.Services;
using CodeBase.Infrastructure.Providers;
using CodeBase.Infrastructure.States.StateInfrastructure;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.States.States
{
    public class GameState : IState
    {
        private readonly List<GameObject> _spawnedPrefabs = new();

        private readonly IInstantiator _instantiator;
        private readonly ILevelProvider _levelProvider;
        private readonly IMatrixOffsetService _matrixOffsetService;

        public GameState(ILevelProvider levelProvider, IInstantiator instantiator, IMatrixOffsetService matrixOffsetService)
        {
            _levelProvider = levelProvider;
            _instantiator = instantiator;
            _matrixOffsetService = matrixOffsetService;
        }

        public void Enter()
        {
            _matrixOffsetService.LoadMatrices(FilePaths.ModelJsonPath,
                FilePaths.SpaceJsonPath,
                out List<Matrix4x4> modelMatrices, 
                out List<Matrix4x4> spaceMatrices);
            
            List<Vector3> foundOffsets = _matrixOffsetService.FindOffsets(modelMatrices, spaceMatrices);
            
            SpawnOffsetPrefabs(foundOffsets);
            
            _matrixOffsetService.ExportOffsetsToJson(FilePaths.OutputOffsetsJsonPath, foundOffsets);
        }

        private void SpawnOffsetPrefabs(List<Vector3> offsets)
        {
            GameObject cubePrefab = _levelProvider.Prefab;
            
            if (cubePrefab == null)
            {
                Debug.LogWarning("Prefab is not assigned! Cannot visualize offsets.");
                return;
            }

            foreach (Vector3 offset in offsets)
            {
                GameObject newPrefab = _instantiator.InstantiatePrefab(cubePrefab, offset, Quaternion.identity, _levelProvider.CubeParent);

                newPrefab.name = $"Offset_{offset.x:F2}_{offset.y:F2}_{offset.z:F2}";
                
                _spawnedPrefabs.Add(newPrefab);
            }

            Debug.Log($"Spawned {offsets.Count} prefabs for visualization");
        }

        public void Exit()
        {
            
        }
    }
}