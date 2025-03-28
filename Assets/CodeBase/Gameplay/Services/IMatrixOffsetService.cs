using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Gameplay.Services
{
    public interface IMatrixOffsetService
    {
        void LoadMatrices(string modelPath, string spacePath, out List<Matrix4x4> modelMatrices, out List<Matrix4x4> spaceMatrices);
        List<Vector3> FindOffsets(List<Matrix4x4> modelMatrices, List<Matrix4x4> spaceMatrices);
        void ExportOffsetsToJson(string outputPath, List<Vector3> offsets);
    }
}