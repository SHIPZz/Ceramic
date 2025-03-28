using System.Collections.Generic;
using System.Linq;
using CodeBase.Gameplay.Common;
using CodeBase.Gameplay.Data;
using CodeBase.Gameplay.Extensions;
using UnityEngine;

namespace CodeBase.Gameplay.Services
{
    public class MatrixOffsetService : IMatrixOffsetService
    {
        private const float OffsetThreshold = 0.99f;
        private readonly IFileService _fileService;

        public MatrixOffsetService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public void LoadMatrices(string modelPath, string spacePath, out List<Matrix4x4> modelMatrices,
            out List<Matrix4x4> spaceMatrices)
        {
            List<MatrixData> modelData = _fileService.LoadMatrixData(modelPath);
            modelMatrices = modelData.Select(data => data.ToMatrix4x4()).ToList();

            List<MatrixData> spaceData = _fileService.LoadMatrixData(spacePath);
            spaceMatrices = spaceData.Select(data => data.ToMatrix4x4()).ToList();

            Debug.Log($"Loaded {modelMatrices.Count} model matrices and {spaceMatrices.Count} space matrices");
        }

        public void ExportOffsetsToJson(string outputPath, Dictionary<OffsetData, bool> offsetThresholds)
        {
            List<OffsetData> targetOffsets = offsetThresholds
                .Where(x => x.Value)
                .Select(x => x.Key)
                .ToList();
            
            _fileService.SaveOffsetsToJson(outputPath, targetOffsets);
            Debug.Log($"Exported {targetOffsets.Count} offsets to {outputPath}");
        }

        public Dictionary<OffsetData, bool> CalculateOffsetThresholds(List<Matrix4x4> modelMatrices, List<Matrix4x4> spaceMatrices)
        {
            var offsetThresholds = new Dictionary<OffsetData, bool>(new OffsetDataComparer());

            foreach (var modelMatrix in modelMatrices)
            {
                (Vector3 position, Quaternion rotation) modelTransform = ExtractTransformFromMatrix(modelMatrix);
                
                foreach (Matrix4x4 spaceMatrix in spaceMatrices)
                {
                    (Vector3 position, Quaternion rotation) spaceTransform = ExtractTransformFromMatrix(spaceMatrix);
                    OffsetData offsetData = CalculateOffsetData(modelTransform, spaceTransform, modelMatrix);
                    
                    if (!offsetThresholds.ContainsKey(offsetData))
                    {
                        offsetThresholds[offsetData] = offsetData.PassesThreshold;
                        LogOffsetFound(offsetData);
                    }
                }
            }

            return offsetThresholds;
        }

        private (Vector3 position, Quaternion rotation) ExtractTransformFromMatrix(Matrix4x4 matrix)
        {
            var position = new Vector3(matrix.m03, matrix.m13, matrix.m23);
            var rotation = matrix.rotation;
            return (position, rotation);
        }

        private OffsetData CalculateOffsetData(
            (Vector3 position, Quaternion rotation) modelTransform,
            (Vector3 position, Quaternion rotation) spaceTransform,
            Matrix4x4 modelMatrix)
        {
            var offset = CalculatePositionOffset(modelTransform.position, spaceTransform.position);
            var rotationEuler = CalculateRotationOffset(modelTransform.rotation, spaceTransform.rotation);
            var passesThreshold = CheckOffsetThreshold(offset, spaceTransform.position, modelMatrix);

            return new OffsetData
            {
                X = offset.x,
                Y = offset.y,
                Z = offset.z,
                XRotation = rotationEuler.x,
                YRotation = rotationEuler.y,
                ZRotation = rotationEuler.z,
                PassesThreshold = passesThreshold
            };
        }

        private Vector3 CalculatePositionOffset(Vector3 modelPosition, Vector3 spacePosition)
        {
            return spacePosition - modelPosition;
        }

        private Vector3 CalculateRotationOffset(Quaternion modelRotation, Quaternion spaceRotation)
        {
            var rotationDiff = spaceRotation * Quaternion.Inverse(modelRotation);
            return rotationDiff.eulerAngles;
        }

        private bool CheckOffsetThreshold(Vector3 offset, Vector3 spacePosition, Matrix4x4 modelMatrix)
        {
            var rotatedOffset = modelMatrix.MultiplyVector(offset);
            return Vector3.Dot(rotatedOffset.normalized, spacePosition.normalized) > OffsetThreshold;
        }

        private void LogOffsetFound(OffsetData offsetData)
        {
            var thresholdStatus = offsetData.PassesThreshold ? "passes" : "does not pass";
            Debug.Log($"Found offset: {offsetData.ToDebugString()} ({thresholdStatus} threshold)");
        }
    }
}