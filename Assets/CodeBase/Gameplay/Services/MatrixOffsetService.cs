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
        private readonly IFileService _fileService;

        public MatrixOffsetService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public void LoadMatrices(string modelPath, string spacePath, out List<Matrix4x4> modelMatrices, out List<Matrix4x4> spaceMatrices)
        {
            List<MatrixData> modelData = _fileService.LoadMatrixData(modelPath);
            modelMatrices = modelData.Select(data => data.ToMatrix4x4()).ToList();

            List<MatrixData> spaceData = _fileService.LoadMatrixData(spacePath);
            spaceMatrices = spaceData.Select(data => data.ToMatrix4x4()).ToList();

            Debug.Log($"Loaded {modelMatrices.Count} model matrices and {spaceMatrices.Count} space matrices");
        }

        public void ExportOffsetsToJson(string outputPath, List<Vector3> offsets)
        {
            List<OffsetData> offsetsData = offsets.Select(offset => offset.ToOffsetData()).ToList();
            _fileService.SaveOffsetsToJson(outputPath, offsetsData);
            Debug.Log($"Exported {offsets.Count} offsets to {outputPath}");
        }
        
        public List<Vector3> FindOffsets(List<Matrix4x4> modelMatrices, List<Matrix4x4> spaceMatrices)
        {
            HashSet<Vector3> foundOffsets = new HashSet<Vector3>();
            Vector3 modelPosition = new Vector3();
            Vector3 spacePosition = new Vector3();

            foreach (Matrix4x4 modelMatrix in modelMatrices)
            {
                modelPosition.Set(modelMatrix.m03, modelMatrix.m13, modelMatrix.m23);
        
                foreach (Matrix4x4 spaceMatrix in spaceMatrices)
                {
                    spacePosition.Set(spaceMatrix.m03, spaceMatrix.m13, spaceMatrix.m23);
                    Vector3 offset = spacePosition - modelPosition;
        
                    if (foundOffsets.Add(offset))
                    {
                        Debug.Log($"Found offset: {offset}");
                    }
                }
            }
        
            return foundOffsets.ToList();
        }
    }
}