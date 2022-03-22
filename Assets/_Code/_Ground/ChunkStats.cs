using UnityEngine;

namespace GroundGeneration
{
    public class ChunkStats
    {
        public int maxX = 16;
        public int maxZ = 16;
        public float baseNoise = 0.02f;
        public float baseNoiseHeight = 4;
        public int elevation = 15;
        public float frequency = 0.005f;
        public Vector3 origin;
    }
}

