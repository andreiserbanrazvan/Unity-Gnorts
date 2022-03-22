using System.Collections.Generic;
using UnityEngine;

namespace GroundGeneration
{
    public class MeshData
    {
        public List<Vector3> vertices = new List<Vector3>();
        public List<int> triangles = new List<int>();
        public List<Vector2> uv = new List<Vector2>();
        public Vector3 origin;

        public void AddVertices(Vector3[] v)
        {
            vertices.AddRange(v);
        }

        public void AddTriangles(int[] t)
        {
            triangles.AddRange(t);
        }

        public void AddUvs(Vector2[] u)
        {
            uv.AddRange(u);
        }

    }
}

