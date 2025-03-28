using CodeBase.Gameplay.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Extensions
{
    public static class MatrixDataExtensions
    {
        public static Matrix4x4 ToMatrix4x4(this MatrixData data)
        {
            Matrix4x4 matrix = new Matrix4x4
            {
                m00 = data.m00,
                m01 = data.m01,
                m02 = data.m02,
                m03 = data.m03,
                m10 = data.m10,
                m11 = data.m11,
                m12 = data.m12,
                m13 = data.m13,
                m20 = data.m20,
                m21 = data.m21,
                m22 = data.m22,
                m23 = data.m23,
                m30 = data.m30,
                m31 = data.m31,
                m32 = data.m32,
                m33 = data.m33
            };

            return matrix;
        }
    }
}