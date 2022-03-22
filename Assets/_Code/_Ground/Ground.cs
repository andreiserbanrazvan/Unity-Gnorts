using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace GroundGeneration
{
    public class Ground : MonoBehaviour
    {
        public int worldX = 4;
        public int worldZ = 4;
        public int chunkX = 16;
        public int chunkZ = 16;
        public float baseNoise = 0.02f;
        public float baseNoiseHeight = 4;
        public int elevation = 15;
        public float frequency = 0.005f;
        Face[,,] grid;
        public Material material;
        public int MaxJobs = 4;
        List<GroundGeneration> toDoJobs = new List<GroundGeneration>();
        List<GroundGeneration> currentJobs = new List<GroundGeneration>();


        void Start()
        {
            CreateWorld();
            AddBoxCollider();
        }

        private void AddBoxCollider()
        {
            var box = this.gameObject.AddComponent<BoxCollider>();
            box.size = new Vector3((float)(worldX * chunkX), 0, (float)(worldZ * chunkZ));           // Square area
            box.center = new Vector3((float)(worldX * chunkX) / 2, 0, (float)(worldZ * chunkZ) / 2); // Square area divided by 2
        }

        void CreateWorld()
        {
            for (int x = 0; x < worldX; x++)
                for (int z = 0; z < worldZ; z++)
                {
                    Vector3 p = Vector3.zero;
                    p.x = x * chunkX + transform.position.x;
                    p.z = z * chunkZ + transform.position.z;
                    p.y = -0.6f;                            // Generate ground bellow buildings
                    RequestWorldGeneration(p);
                }
        }

        void Update()
        {
            int i = 0;
            while (i < currentJobs.Count)
            {
                if (currentJobs[i].jobDone)
                {
                    currentJobs[i].NotifyComplete();
                    currentJobs.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }

            if (toDoJobs.Count > 0 && currentJobs.Count < MaxJobs)
            {
                GroundGeneration job = toDoJobs[0];
                toDoJobs.RemoveAt(0);
                currentJobs.Add(job);

                ChunkStats s = new ChunkStats();

                Thread jobThread = new Thread(job.StartCreatingWorld);
                jobThread.Start();
            }
        }

        public void LoadMeshData(Face[,,] createdGrid, MeshData d)
        {
            grid = createdGrid;

            GameObject go = new GameObject(d.origin.ToString());
            go.transform.SetParent(this.gameObject.transform);
            go.layer = 9;

            MeshRenderer renderer = go.AddComponent<MeshRenderer>();
            MeshFilter filter = go.AddComponent<MeshFilter>();

            renderer.material = material;

            Mesh mesh = new Mesh()
            {
                vertices = d.vertices.ToArray(),
                uv = d.uv.ToArray(),
                triangles = d.triangles.ToArray()
            };
            mesh.RecalculateNormals();

            filter.mesh = mesh;
        }

        public Face GetBlock(int x, int y, int z)
        {
            if (x < 0 || y < 0 || z < 0 || x > chunkX - 1 || z > chunkZ - 1 || y > elevation - 1)
            {
                return null;
            }

            return grid[x, y, z];
        }

        public void RequestWorldGeneration(Vector3 origin)
        {
            ChunkStats s = new ChunkStats
            {
                maxX = chunkX,
                maxZ = chunkZ,
                baseNoise = baseNoise,
                baseNoiseHeight = baseNoiseHeight,
                elevation = elevation,
                frequency = frequency,
                origin = origin
            };


            GroundGeneration wg = new GroundGeneration(s, LoadMeshData);
            toDoJobs.Add(wg);
        }
    }
}
