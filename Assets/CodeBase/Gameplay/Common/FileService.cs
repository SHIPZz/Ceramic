using System.Collections.Generic;
using System.IO;
using CodeBase.Gameplay.Data;
using Newtonsoft.Json;

namespace CodeBase.Gameplay.Common
{
    public class FileService : IFileService
    {
        public List<MatrixData> LoadMatrixData(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found at path: {filePath}");
            }

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<MatrixData>>(json);
        }

        public void SaveOffsetsToJson(string filePath, List<OffsetData> offsets)
        {
            string json = JsonConvert.SerializeObject(offsets, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}