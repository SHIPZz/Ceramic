using System.Collections.Generic;
using System.Linq;
using CodeBase.Constants;
using CodeBase.Gameplay;
using CodeBase.Gameplay.Data;
using CodeBase.Gameplay.Extensions;
using CodeBase.Gameplay.Services;
using CodeBase.Infrastructure.Providers;
using CodeBase.Infrastructure.States.StateInfrastructure;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.States.States
{
    public class GameState : IState
    {
        private readonly List<CubeVisualizationView> _spawnedPrefabs = new();

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

            List<OffsetData> foundOffsets = _matrixOffsetService.FindOffsets(modelMatrices, spaceMatrices);
            
            SpawnOffsetPrefabs(foundOffsets);

            List<OffsetData> targetOffsets = foundOffsets.Where(x => x.PassesThreshold).ToList();
            
            _matrixOffsetService.ExportOffsetsToJson(FilePaths.OutputOffsetsJsonPath, targetOffsets);
        }

        private void SpawnOffsetPrefabs(List<OffsetData> offsets)
        {
            CubeVisualizationView cubePrefab = _levelProvider.CubePrefab;
            
            foreach (OffsetData offsetData in offsets)
            {
                CubeVisualizationView createdCube = _instantiator.InstantiatePrefabForComponent<CubeVisualizationView>(
                    cubePrefab, 
                    offsetData.ToPosition(), 
                    offsetData.ToQuaternion(), 
                    _levelProvider.CubeParent
                );

                createdCube.name = offsetData.ToDebugString();

                if (offsetData.PassesThreshold) 
                    createdCube.SetColor(Color.blue);
                
                _spawnedPrefabs.Add(createdCube);
            }

            Debug.Log($"Spawned {offsets.Count} prefabs for visualization");
        }

        public void Exit()
        {
            
        }
    }
}