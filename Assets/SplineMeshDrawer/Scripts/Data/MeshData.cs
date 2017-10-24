using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace geniikw.UIMeshLab
{
    /// <summary>
    /// vertexes
    ///      position
    ///      normal
    ///      uv
    ///      color
    /// triangles
    /// 
    /// Line <-> MeshData <-> world, canvas, Gizmo.
    /// </summary>
    public struct MeshData
    {
        public List<Vertex> vertexes;
        public List<int> triangles;
        
        public IEnumerable<int[]> Triangles{
            get
            {
                if (triangles.Count <= 0)
                    yield break;

                for (int i = 0; i < triangles.Count - 1; i += 3)
                {
                    yield return new int[]{triangles[i], triangles[i + 1], triangles[i + 2]};
                }
            }
        }

        public static MeshData Void()
        {
            return new MeshData
            {
                vertexes = new List<Vertex>(),
                triangles = new List<int>()
            };
        }

        public static MeshData Quad(Vertex p0, Vertex p1, Vertex p2, Vertex p3)
        {
            return new MeshData
            {
                vertexes = new List<Vertex> { p0, p1, p2, p3 },
                triangles = new List<int> { 0, 2, 1, 1, 2, 3 }
            };
        }

        public static MeshData Triangle(Vertex p0, Vertex p1, Vertex p2)
        {
            return new MeshData
            {
                vertexes = new List<Vertex> { p0, p1, p2 },
                triangles = new List<int> { 0, 2, 1 }
            };
        }

        public static MeshData operator +(MeshData ls, MeshData rs){
            var tc = ls.vertexes.Count;
            ls.vertexes.AddRange(rs.vertexes);
            var tt = rs.triangles.Select(t => t + tc).ToArray();
            ls.triangles.AddRange(tt);
            return ls;
        }
    }
}