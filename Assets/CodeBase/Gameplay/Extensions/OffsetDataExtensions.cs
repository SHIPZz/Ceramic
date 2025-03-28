using CodeBase.Gameplay.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Extensions
{
    public static class OffsetDataExtensions
    {
        public static Vector3 ToPosition(this OffsetData offsetData)
        {
            return new Vector3(offsetData.X, offsetData.Y, offsetData.Z);
        }

        public static Vector3 ToRotation(this OffsetData offsetData)
        {
            return new Vector3(offsetData.XRotation, offsetData.YRotation, offsetData.ZRotation);
        }

        public static Quaternion ToQuaternion(this OffsetData offsetData)
        {
            return Quaternion.Euler(offsetData.ToRotation());
        }

        public static string ToDebugString(this OffsetData offsetData)
        {
            return $"Offset_{offsetData.X:F2}_{offsetData.Y:F2}_{offsetData.Z:F2}" +
                   $"_Rot_{offsetData.XRotation:F2}_{offsetData.YRotation:F2}_{offsetData.ZRotation:F2}";
        }
    }
} 