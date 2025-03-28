using System.Collections.Generic;
using CodeBase.Gameplay.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Services
{
    public interface IMatrixOffsetService
    {
        void LoadMatrices(string modelPath, string spacePath, out List<Matrix4x4> modelMatrices, out List<Matrix4x4> spaceMatrices);
        Dictionary<OffsetData, bool> CalculateOffsetThresholds(List<Matrix4x4> modelMatrices, List<Matrix4x4> spaceMatrices);
        void ExportOffsetsToJson(string outputPath, Dictionary<OffsetData, bool> offsetThresholds);
    }
}