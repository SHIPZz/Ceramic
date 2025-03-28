using System.Collections.Generic;
using CodeBase.Gameplay.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Services
{
    public interface IMatrixOffsetService
    {
        void LoadMatrices(string modelPath, string spacePath, out List<Matrix4x4> modelMatrices, out List<Matrix4x4> spaceMatrices);
        List<OffsetData> FindOffsets(List<Matrix4x4> modelMatrices, List<Matrix4x4> spaceMatrices);
        void ExportOffsetsToJson(string outputPath, List<OffsetData> offsets);
    }
}