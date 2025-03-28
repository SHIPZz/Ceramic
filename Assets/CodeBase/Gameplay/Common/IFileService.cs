using System.Collections.Generic;
using CodeBase.Gameplay.Data;

namespace CodeBase.Gameplay.Common
{
    public interface IFileService
    {
        List<MatrixData> LoadMatrixData(string filePath);
        void SaveOffsetsToJson(string filePath, List<OffsetData> offsets);
    }
}