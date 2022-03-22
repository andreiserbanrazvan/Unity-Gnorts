using UnityEngine;

namespace GroundGeneration
{
    public class Face
    {
        public int x;
        public int y;
        public int z;
        public Vector3 worldPosition;
        public bool isSolid;

        public virtual void LoadFace(MeshData d, GroundGeneration w)
        {
            Vector3 o = worldPosition;
            FaceUp(d, o);
        }

        public void FaceUp(MeshData d, Vector3 o)
        {
            Vector3[] v = new Vector3[4];
            v[0] = new Vector3(o.x - .5f, o.y + .5f, o.z + .5f);
            v[1] = new Vector3(o.x + .5f, o.y + .5f, o.z + .5f);
            v[2] = new Vector3(o.x + .5f, o.y + .5f, o.z - .5f);
            v[3] = new Vector3(o.x - .5f, o.y + .5f, o.z - .5f);

            d.AddVertices(v);

            AddTrianglesAndUVs(d, v);
        }

        private void AddTrianglesAndUVs(MeshData d, Vector3[] v)
        {
            int[] t = new int[6];
            t[0] = d.vertices.Count - 4;
            t[1] = d.vertices.Count - 3;
            t[2] = d.vertices.Count - 2;

            t[3] = d.vertices.Count - 4;
            t[4] = d.vertices.Count - 2;
            t[5] = d.vertices.Count - 1;

            d.AddTriangles(t);

            Vector2[] uv = new Vector2[4];

            uv[0] = new Vector2(0, 0);
            uv[1] = new Vector2(0, 1);
            uv[2] = new Vector2(1, 1);
            uv[3] = new Vector2(1, 0);

            d.AddUvs(uv);
        }
    }

}

