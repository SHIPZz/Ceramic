using System;

namespace CodeBase.Gameplay.Data
{
    [Serializable]
    public class OffsetData
    {
        public float X;
        public float Y;
        public float Z;

        public float XRotation;
        public float YRotation;
        public float ZRotation;

        public bool PassesThreshold;
    }
}