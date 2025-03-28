using System.Collections.Generic;
using CodeBase.Gameplay.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Services
{
    public class OffsetDataComparer : IEqualityComparer<OffsetData>
    {
        public bool Equals(OffsetData x, OffsetData y)
        {
            return Mathf.Abs(x.X - y.X) < Mathf.Epsilon &&
                   Mathf.Abs(x.Y - y.Y) < Mathf.Epsilon &&
                   Mathf.Abs(x.Z - y.Z) < Mathf.Epsilon &&
                   Mathf.Abs(x.XRotation - y.XRotation) < Mathf.Epsilon &&
                   Mathf.Abs(x.YRotation - y.YRotation) < Mathf.Epsilon &&
                   Mathf.Abs(x.ZRotation - y.ZRotation) < Mathf.Epsilon;
        }

        public int GetHashCode(OffsetData obj)
        {
            return obj.X.GetHashCode() ^ obj.Y.GetHashCode() ^ obj.Z.GetHashCode() ^
                   obj.XRotation.GetHashCode() ^ obj.YRotation.GetHashCode() ^ obj.ZRotation.GetHashCode();
        }
    }
}