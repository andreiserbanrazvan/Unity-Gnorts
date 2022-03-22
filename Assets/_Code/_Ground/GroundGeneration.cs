using Simplex;
using System.Collections.Generic;
using UnityEngine;

namespace GroundGeneration
{
    public class GroundGeneration
    {
        Face[,,] grid;

        Vector3 point;
        ChunkStats s;
        MeshData meshData;
        public volatile bool jobDone;
        public delegate void WorldGenerationCallBack(Face[,,] grid, MeshData d);
        WorldGenerationCallBack finishCallback;

        public void JobGeneration()
        {
            StartCreatingWorld();
        }

        public GroundGeneration(ChunkStats stats, WorldGenerationCallBack callback)
        {
            s = stats;
            finishCallback = callback;
        }

        public void StartCreatingWorld()
        {
            meshData = CreateChunk();
            jobDone = true;
        }

        public void NotifyComplete()
        {
            finishCallback(grid, meshData);
        }

        int GetNoise(float x, float y, float z, float scale, int max)
        {
            return Mathf.FloorToInt((Noise.Generate(x * scale, y * scale, z * scale) + 1) * (max / 2f));
        }

        MeshData CreateChunk()
        {
            grid = new Face[s.maxX, s.elevation, s.maxZ];

            List<Face> blocks = new List<Face>();
            for (int x = 0; x < s.maxX; x++)
            {
                for (int z = 0; z < s.maxZ; z++)
                {
                    Face b = new Face();
                    b.x = x;
                    b.z = z;

                    Vector3 targetPosition = s.origin;
                    targetPosition.x += x * 1;
                    targetPosition.z += z * 1;
                    float height = 0;

                    Vector3 noisePosition = s.origin;
                    noisePosition.x += targetPosition.x;
                    noisePosition.z += targetPosition.z;

                    height += GetNoise(noisePosition.x, 0, noisePosition.z, s.frequency, s.elevation);

                    targetPosition.y += height;
                    b.worldPosition = targetPosition;
                    b.y = Mathf.RoundToInt(height);

                    grid[x, b.y, z] = b;
                    blocks.Add(b);
                }
            }

            MeshData d = new MeshData();
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].LoadFace(d, this);
            }

            return d;
        }

        public Face GetBlock(int x, int y, int z)
        {
            if (x < 0 || y < 0 || z < 0 || x > s.maxX - 1 || z > s.maxZ - 1 || y > s.elevation - 1)
            {
                return null;
            }

            return grid[x, y, z];
        }
    }
}

